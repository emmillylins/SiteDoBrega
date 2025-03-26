using Domain.Entities;
using Infrastructure.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.DTOs;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : MainController
    {
        private readonly IAuthService _authService;

        public AuthController(INotificador notificador, IAuthService authService) : base(notificador)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO login)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);

                var token = await _authService.Login(login);
                return CustomResponse(token);
            }
            catch (Exception ex)
            {
                NotificarErro(ex.Message);
                return CustomResponse();
            }
        }

        [AllowAnonymous]
        [HttpPost("logout/{token}")]
        public async Task<IActionResult> Logout(string token)
        {
            try
            {
                await _authService.Logout(token);
                return CustomResponse("Logout realizado com sucesso.");
            }
            catch (Exception ex)
            {
                NotificarErro(ex.Message);
                return CustomResponse();
            }
        }

        [AllowAnonymous]
        [HttpPost("cadastro")]
        public async Task<ActionResult> Cadastro([FromBody] RegisterDTO register)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);

                var result = await _authService.Register(register);
                return CustomResponse(result);
            }
            catch (Exception ex)
            {
                NotificarErro(ex.Message);
                return CustomResponse();
            }
        }

        [AllowAnonymous]
        [HttpGet("usuarios")]
        public async Task<ActionResult> ListarUsuarios()
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);

                var result = await _authService.GetUsersAsync();
                return CustomResponse(result);
            }
            catch (Exception ex)
            {
                NotificarErro(ex.Message);
                return CustomResponse();
            }
        }
    }
}
