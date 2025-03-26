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
    [Route("api/categorias")]
    public class CategoriaController : MainController
    {
        private readonly ICategoriaService _service;
        public CategoriaController(INotificador notificador, ICategoriaService service) : base(notificador)
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
                return CustomResponse(await _service.GetAsync<CategoriaDTO>());
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
                return CustomResponse(await _service.GetAsync<CategoriaDTO>(id));
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
        public async Task<ActionResult> Inserir(CreateCategoriaDTO DTO)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);
                var result = await _service.InsertAsync<CreateCategoriaDTO, CategoriaDTO, CategoriaValidator>(DTO);
                return CustomResponse(result);
            }
            catch (Exception ex)
            {
                NotificarErro(ex.Message);
                return CustomResponse();
            }
        }

        [HttpPut]
        public async Task<ActionResult> Atualizar(CategoriaDTO DTO)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);
                //var username = HttpContext.User.Identity.Name;
                var result = await _service.UpdateAsync<CategoriaDTO, CategoriaDTO, CategoriaValidator>(DTO);
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
