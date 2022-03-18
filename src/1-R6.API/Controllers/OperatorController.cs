using System.Threading.Tasks;
using R6.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using R6.Services.Interfaces;
using AutoMapper;
using R6.Services.DTO;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using R6.Core.Communication.Messages.Notifications;

namespace R6.API.Controllers
{

    [ApiController]
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
        [Route("/api/v1/users/get-all")]
        public async Task<IActionResult> GetAsync()
        {
            var allUsers = await _operatorService.GetAllAsync();

            if (HasNotifications())
                return Result();

            return Ok(new ResultViewModel
            {
                Message = "Usuários encontrados com sucesso!",
                Success = true,
                Data = allUsers.Value
            });
        }
    }
}