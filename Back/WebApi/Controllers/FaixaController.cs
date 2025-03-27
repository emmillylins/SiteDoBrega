using Application.DTOs;
using Application.Interfaces;
using Domain.Validators;
using Infrastructure.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
        [HttpGet("{categoriaId}")]
        public async Task<ActionResult> Obter(int categoriaId)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);
                return CustomResponse(await _service.GetAsync<FaixaDTO>(categoriaId));
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
                if (ex is SqlException) NotificarErro(ex.InnerException?.Message ?? string.Empty);
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
                if (ex is SqlException) NotificarErro(ex.InnerException?.Message ?? string.Empty);
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
                if (ex is SqlException) NotificarErro(ex.InnerException?.Message ?? string.Empty);
                NotificarErro(ex.Message);
                return CustomResponse();
            }
        }
    }
}
