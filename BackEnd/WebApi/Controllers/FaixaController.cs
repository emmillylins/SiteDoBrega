using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Validators;
using Infrastructure.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data.SqlClient;
using WebApi.DTOs;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/faixas")]
    public class FaixaController : MainController
    {
        private readonly IFaixaService _service;
        private readonly IMapper _mapper;

        public FaixaController(IFaixaService service, INotificador notificador, IMapper mapper) : base(notificador)
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

                var entities = _service.Get<FaixaDto>().ToList();
                if (entities.IsNullOrEmpty()) return NotFound(new { success = false, errors = "Nenhum registro encontrado." });

                return CustomResponse(entities);
            }
            catch (Exception ex)
            {
                NotificarErro(ex.Message);
                return CustomResponse();
            }
        }

        [HttpGet("por-categoria/{categoriaId:int}")]
        public async Task<ActionResult> ListarPorCategoria(int categoriaId)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);

                var entities = _service.Get<FaixaDto>().Where(f => f.CategoriaId == categoriaId).ToList();
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

                var entity = _service.Get<FaixaDto>().FirstOrDefault(e => e.Id == id);
                if (entity is null) return NotFound(new { success = false, errors = $"O registro {id} não foi encontrado." });

                return CustomResponse(entity);
            }
            catch (Exception ex)
            {
                NotificarErro(ex.Message);
                return CustomResponse();
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<FaixaDto>> Excluir(int id)
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
        public async Task<ActionResult> Inserir(List<CreateFaixaDto> dtos)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);
                return CustomResponse(_service.Add<CreateFaixaDto, FaixaDto, FaixaValidator>(dtos));
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
        public async Task<ActionResult> Atualizar(List<FaixaDto> dtos)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);
                return CustomResponse(_service.Update<FaixaDto, FaixaDto, FaixaValidator>(dtos));
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