using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using EscNet.Cryptography.Interfaces;
using R6.Core.Communication.Mediator.Interfaces;
using R6.Core.Communication.Messages.Notifications;
using R6.Core.Enums;
using R6.Core.Structs;
using R6.Core.Validations.Message;
using R6.Domain.Entities;
using R6.Infra.Interfaces;
using R6.Services.DTO;
using R6.Services.Excpetion;
using R6.Services.Interfaces;

namespace R6.Services.Services
{
    public class OperatorService : IOperatorService
    {
        private readonly IMapper _mapper;
        private readonly IOperatorRepository _operatorRepository;
        private readonly IRijndaelCryptography _rijndaelCryptography;
        private readonly IMediatorHandler _mediator;

        public OperatorService(
            IMapper mapper,
            IOperatorRepository operatorRepository,
            IRijndaelCryptography rijndaelCryptography,
            IMediatorHandler mediator)
        {
            _mapper = mapper;
            _operatorRepository = operatorRepository;
            _rijndaelCryptography = rijndaelCryptography;
            _mediator = mediator;
        }

        public async Task<Optional<OperatorDto>> CreateAsync(OperatorDto operatorDto)
        {
            Expression<Func<Operator, bool>> filter = op
                => op.Name.ToLower() == operatorDto.Name.ToLower();

            var operatorExists = await _operatorRepository.GetAsync(filter);

            if (operatorExists != null)
            {
                await _mediator.PublishDomainNotificationAsync(new DomainNotification(
                    ErrorMessages.OperatorAlreadyExists,
                    DomainNotificationType.OperatorAlreadyExists));

                throw new AppException("Operador já existe");
            }

            var op = _mapper.Map<Operator>(operatorDto);
            op.Validate();

            if (!op.IsValid)
            {
                await _mediator.PublishDomainNotificationAsync(new DomainNotification(
                   ErrorMessages.UserInvalid(op.ErrorsToString()),
                   DomainNotificationType.UserInvalid));

                throw new AppException("Campos inválidos: ", op.ErrorsToString());
            }

            var userCreated = await _operatorRepository.CreateAsync(op);

            return _mapper.Map<OperatorDto>(userCreated);
        }

        public async Task<Optional<IList<OperatorDto>>> GetAllAsync()
        {
            var allOperators = await _operatorRepository.GetAllAsync();
            var allOperatorsDto = _mapper.Map<IList<OperatorDto>>(allOperators);

            return new Optional<IList<OperatorDto>>(allOperatorsDto);
        }

        public async Task<Optional<OperatorDto>> GetAsync(long id)
        {
            var op = await _operatorRepository.GetAsync(id);
            var operatorDto = _mapper.Map<OperatorDto>(op);

            return new Optional<OperatorDto>(operatorDto);
        }

        public async Task<Optional<IList<OperatorDto>>> SearchBySpeedAsync(SpeedType speed)
        {
            Expression<Func<Operator, bool>> filter = op
                => op.Speed == speed;

            var operators = await _operatorRepository.SearchAsync(filter);
            var operatorsDto = _mapper.Map<IList<OperatorDto>>(operators);

            return new Optional<IList<OperatorDto>>(operatorsDto);
        }
    }
}