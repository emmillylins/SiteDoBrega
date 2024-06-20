using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Validators;
using Infrastructure.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data.SqlClient;
using WebApi.DTOs;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/categorias")]
    public class CategoriaController : MainController
    {
        private readonly ICategoriaService _service;
        private readonly IMapper _mapper;

        public CategoriaController(ICategoriaService service, INotificador notificador, IMapper mapper) : base(notificador)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Listar()
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);

                var entities = _service.Get<CategoriaDto>().ToList();
                if (entities.IsNullOrEmpty()) return NotFound(new { success = false, errors = "Nenhum registro encontrado." });

                return CustomResponse(entities);
            }
            catch (Exception ex)
            {
                NotificarErro(ex.Message);
                return CustomResponse();
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Obter(int id)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);

                var entity = _service.Get<CategoriaDto>().FirstOrDefault(e => e.Id == id);
                if (entity is null) return NotFound(new { success = false, errors = $"O registro {id} não foi encontrado." });

                return CustomResponse(entity);
            }
            catch (Exception ex)
            {
                NotificarErro(ex.Message);
                return CustomResponse();
            }
        }

        /// <summary>
        /// Exclusão lógica
        /// </summary>
        /// <param name="id">O id do item</param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoriaDto>> Excluir(int id)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);
                _service.Delete(id.ToString());
                return CustomResponse(true);
            }
            catch (NotFoundException nf)
            {
                return NotFound(new { success = false, errors = nf.Message });
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) return Conflict(new { success = false, errors = ex.InnerException.Message });
                NotificarErro(ex.Message);
                return CustomResponse();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Inserir(List<CreateCategoriaDto> dtos)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);
                return CustomResponse(_service.Add<CreateCategoriaDto, CategoriaDto, CategoriaValidator>(dtos));
            }
            catch (ConflictException c)
            {
                return Conflict(new { success = false, errors = c.Message });
            }
            catch (NotFoundException nf)
            {
                return NotFound(new { success = false, errors = nf.Message });
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) return Conflict(ex.InnerException.Message);
                NotificarErro(ex.Message);
                return CustomResponse();
            }

        }

        [HttpPut]
        public async Task<ActionResult> Atualizar(List<CategoriaDto> dtos)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);
                return CustomResponse(_service.Update<CategoriaDto, CategoriaDto, CategoriaValidator>(dtos));
            }
            catch (ConflictException c)
            {
                return Conflict(new { success = false, errors = c.Message });
            }
            catch (NotFoundException nf)
            {
                return NotFound(new { success = false, errors = nf.Message });
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) return Conflict(new { success = false, errors = ex.InnerException.Message });
                NotificarErro(ex.Message);
                return CustomResponse();
            }
        }
    }
}