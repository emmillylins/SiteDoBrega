﻿using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Validators;
using Infrastructure.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using WebApi.DTOs;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : MainController
    {
        private readonly IUsuarioService _service;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioService service, INotificador notificador, IMapper mapper) : base(notificador)
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

                var entities = _service.Get<UsuarioDto>().ToList();
                if (entities.IsNullOrEmpty()) return NotFound(new { success = false, errors = "Nenhum registro encontrado." });

                return CustomResponse(entities);
            }
            catch (Exception ex)
            {
                NotificarErro(ex.Message);
                return CustomResponse();
            }
        }

        [HttpGet("{username}")]
        public async Task<ActionResult> Obter(string username)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);

                var entity = _service.Get<UsuarioDto>().FirstOrDefault(e => e.NomeUsuario == username);
                if (entity is null) return NotFound(new { success = false, errors = $"O usuário {username} não foi encontrado." });

                return CustomResponse(entity);
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) return Conflict(new { success = false, errors = ex.InnerException.Message });
                NotificarErro(ex.Message);
                return CustomResponse();
            }
        }

        /// <summary>
        /// Exclusão lógica
        /// </summary>
        /// <param name="username">O NomeUsuario</param>
        /// <returns></returns>
        [HttpDelete("{username}")]
        public async Task<ActionResult<UsuarioDto>> Excluir(string username)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);
                _service.Delete(username);
                return CustomResponse(true);
            }
            catch (NotFoundException nf)
            {
                return NotFound(new { success = false, errors = nf.Message });
            }
            catch (Exception ex)
            {
                NotificarErro(ex.Message);
                return CustomResponse();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Inserir(List<UsuarioDto> dtos)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);
                return CustomResponse(_service.Add<UsuarioDto, UsuarioDto, UsuarioValidator>(dtos));
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
        public async Task<ActionResult> Atualizar(List<UsuarioDto> dtos)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);
                return CustomResponse(_service.Update<UsuarioDto, UsuarioDto, UsuarioValidator>(dtos));
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
