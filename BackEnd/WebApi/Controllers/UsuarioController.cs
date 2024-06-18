using Application.Interfaces;
using AutoMapper;
using Domain.Validators;
using Infrastructure.Interfaces;
using Infrastructure.Notifications;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : MainController
    {
        private readonly IUsuarioRepository _repository;
        private readonly IUsuarioService _service;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioRepository repository, IUsuarioService service, INotificador notificador, IMapper mapper) : base(notificador)
        {
            _repository = repository;
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Listar()
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);
                return CustomResponse(_repository.Select());
            }
            catch (MultipleValidationExceptions ex)
            {
                List<ValidationError> validationErrors = ex.ValidationErrors;

                return NotFound(new
                {
                    success = false,
                    errors = validationErrors.Select(error => error.ErrorMessage).ToList()
                });
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> Obter(int id)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);
                return CustomResponse(_repository.Select(id));
            }
            catch (MultipleValidationExceptions ex)
            {
                List<ValidationError> validationErrors = ex.ValidationErrors;

                return NotFound(new
                {
                    success = false,
                    errors = validationErrors.Select(error => error.ErrorMessage).ToList()
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Inserir([FromBody] CreateUsuarioDto dto)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);
                return CustomResponse(_service.Add<CreateUsuarioDto, UsuarioDto, UsuarioValidator>(dto));
            }
            catch (MultipleValidationExceptions ex)
            {
                List<ValidationError> validationErrors = ex.ValidationErrors;

                return BadRequest(new
                {
                    success = false,
                    errors = validationErrors.Select(error => error.ErrorMessage).ToList()
                });
            }

        }

        [HttpPut]
        public async Task<ActionResult> Atualizar(UsuarioDto Usuario)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);
                return CustomResponse(_service.Update<UsuarioDto, UsuarioDto, UsuarioValidator>(Usuario));
            }
            catch (MultipleValidationExceptions ex)
            {
                List<ValidationError> validationErrors = ex.ValidationErrors;

                return BadRequest(new
                {
                    success = false,
                    errors = validationErrors.Select(error => error.ErrorMessage).ToList()
                });
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<UsuarioDto>> Excluir(int id)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);

                _service.Delete(id);
                return CustomResponse(true);
            }
            catch (MultipleValidationExceptions ex)
            {
                List<ValidationError> validationErrors = ex.ValidationErrors;

                return NotFound(new
                {
                    success = false,
                    errors = validationErrors.Select(error => error.ErrorMessage).ToList()
                });
            }
        }
    }
}
