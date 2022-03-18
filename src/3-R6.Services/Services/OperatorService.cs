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
using R6.Services.Interfaces;
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

        public async Task<Optional<IList<OperatorDto>>> GetAllAsync()
        {
            var allOperators = await _operatorRepository.GetAllAsync();
            var allOperatorsDto = _mapper.Map<IList<OperatorDto>>(allOperators);

            return new Optional<IList<OperatorDto>>(allOperatorsDto);
        }
    }
}