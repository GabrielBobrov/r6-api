using R6.API.Token;
using R6.API.Utilities;
using R6.API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace R6.API.Controllers
{
    [ApiController]
    [Route("/api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenGenerator _tokenGenerator;

        public AuthController(IConfiguration configuration, ITokenGenerator tokenGenerator)
        {
            _configuration = configuration;
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginViewModel loginViewModel)
        {
            try
            {
            var tokenLogin = _configuration["Jwt:Login"];
            var tokenPassword = _configuration["Jwt:Password"];

            if (loginViewModel.Login == tokenLogin && loginViewModel.Password == tokenPassword)
                return Ok(new ResultViewModel
                {
                    Message = "Usuário autenticado com sucesso!",
                    Success = true,
                    Data = new
                    {
                        Token = _tokenGenerator.GenerateToken(),
                        TokenExpires = DateTime.UtcNow.AddHours(int.Parse(_configuration["Jwt:HoursToExpire"]))
                    }
                });
            else
                return StatusCode(401, Responses.UnauthorizedErrorMessage());
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
                throw;
            }
        }
    }
}