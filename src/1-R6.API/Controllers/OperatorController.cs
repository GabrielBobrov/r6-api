using System.Threading.Tasks;
using R6.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using R6.Services.Interfaces;
using AutoMapper;
using R6.Services.DTO;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using R6.Core.Communication.Messages.Notifications;
using R6.API.Messages;
using R6.Core.Enums;

namespace R6.API.Controllers
{

    [ApiController]
    [Authorize]
    [Route("/api/v1/operators")]
    public class OperatorController : BaseController
    {

        private readonly IMapper _mapper;
        private readonly IOperatorService _operatorService;

        public OperatorController(
            IMapper mapper,
            IOperatorService operatorService,
            INotificationHandler<DomainNotification> domainNotificationHandler)
            : base(domainNotificationHandler)
        {
            _mapper = mapper;
            _operatorService = operatorService;
        }


        [HttpGet]
        [Route("get-by-speed")]
        public async Task<IActionResult> GetBySpeedAsync([FromQuery] SpeedType speed)
        {
            var operators = await _operatorService.SearchBySpeedAsync(speed);

            if (HasNotifications())
                return Result();

            return Ok(new ResultViewModel
            {
                Message = ResponseMessages.SuccessMessageGetAllOperators,
                Success = true,
                Data = operators.Value
            });
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            var op = await _operatorService.GetAsync(id);

            if (HasNotifications())
                return Result();

            return Ok(new ResultViewModel
            {
                Message = ResponseMessages.SuccessMessageGetOperator,
                Success = true,
                Data = op.Value
            });
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateOperatorViewModel operatorViewModel)
        {
            var operatorDto = _mapper.Map<OperatorDto>(operatorViewModel);
            var operatorCreated = await _operatorService.CreateAsync(operatorDto);

            if (HasNotifications())
                return Result();

            return Created(new ResultViewModel
            {
                Message = "Usuário criado com sucesso!",
                Success = true,
                Data = operatorCreated.Value
            });
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var allOperators = await _operatorService.GetAllAsync();

            if (HasNotifications())
                return Result();

            return Ok(new ResultViewModel
            {
                Message = ResponseMessages.SuccessMessageGetAllOperators,
                Success = true,
                Data = allOperators.Value
            });
        }
    }
}