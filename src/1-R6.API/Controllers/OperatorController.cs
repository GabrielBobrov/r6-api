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

namespace R6.API.Controllers
{

    [ApiController]
    [Route("/api/v1/operators")]
    public class OperatorController : BaseController
    {

        private readonly IMapper _mapper;
        private readonly IOperatorService _operatorService;

        public OperatorController(
            IMapper mapper,
            IOperatorService userService,
            INotificationHandler<DomainNotification> domainNotificationHandler)
            : base(domainNotificationHandler)
        {
            _mapper = mapper;
            _operatorService = userService;
        }


        [HttpGet]
        [Authorize]
        [Route("get-all")]
        public async Task<IActionResult> GetAsync()
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

        [HttpPost]
        [Authorize]
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
    }
}