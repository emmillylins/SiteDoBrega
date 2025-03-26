using Application.DTOs;
using Application.Interfaces;
using Domain.Validators;
using Infrastructure.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Main;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/faixas")]
    public class FaixaController : MainController
    {
        private readonly IFaixaService _service;
        public FaixaController(INotificador notificador, IFaixaService service) : base(notificador)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> Listar()
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);
                return CustomResponse(await _service.GetAsync<FaixaDTO>());
            }
            catch (Exception ex)
            {
                NotificarErro(ex.Message);
                return CustomResponse();
            }
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult> Obter(int id)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);
                return CustomResponse(await _service.GetAsync<FaixaDTO>(id));
            }
            catch (Exception ex)
            {
                NotificarErro(ex.Message);
                return CustomResponse();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Excluir(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return CustomResponse();
            }
            catch (Exception ex)
            {
                NotificarErro(ex.Message);
                return CustomResponse();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Inserir(CreateFaixaDTO DTO)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);
                var result = await _service.InsertAsync<CreateFaixaDTO, FaixaDTO, FaixaValidator>(DTO);
                return CustomResponse(result);
            }
            catch (Exception ex)
            {
                NotificarErro(ex.Message);
                return CustomResponse();
            }
        }

        [HttpPut]
        public async Task<ActionResult> Atualizar(FaixaDTO DTO)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);
                //var username = HttpContext.User.Identity.Name;
                var result = await _service.UpdateAsync<FaixaDTO, FaixaDTO, FaixaValidator>(DTO);
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
