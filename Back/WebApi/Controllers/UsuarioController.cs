//using Application.DTOs;
//using Application.Interfaces;
//using AutoMapper;
//using Domain.Validators;
//using Infrastructure.Notifications;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using WebApi.Controllers.Main;

//namespace WebApi.Controllers
//{
//    [ApiController]
//    [Route("api/usuarios")]
//    public class UsuarioController : MainController
//    {
//        private readonly IUsuarioService _service;
//        private readonly IMapper _mapper;
//        public UsuarioController(INotificador notificador, IUsuarioService service, IMapper mapper) : base(notificador)
//        {
//            _service = service;
//            _mapper = mapper;
//        }

//        [AllowAnonymous]
//        [HttpGet]
//        public async Task<ActionResult> Listar()
//        {
//            try
//            {
//                if (!ModelState.IsValid) return CustomResponse(ModelState);
//                return CustomResponse(await _service.GetAsync<UsuarioDTO>());
//            }
//            catch (Exception ex)
//            {
//                NotificarErro(ex.Message);
//                return CustomResponse();
//            }
//        }

//        [AllowAnonymous]
//        [HttpGet("{id}")]
//        public async Task<ActionResult> Obter(int id)
//        {
//            try
//            {
//                if (!ModelState.IsValid) return CustomResponse(ModelState);
//                return CustomResponse(await _service.GetAsync<UsuarioDTO>(id));
//            }
//            catch (Exception ex)
//            {
//                NotificarErro(ex.Message);
//                return CustomResponse();
//            }
//        }

//        [AllowAnonymous]
//        [HttpDelete("{id}")]
//        public async Task<ActionResult> Excluir(int id)
//        {
//            try
//            {
//                _service.Delete(id);
//                return CustomResponse();
//            }
//            catch (Exception ex)
//            {
//                NotificarErro(ex.Message);
//                return CustomResponse();
//            }
//        }

//        [AllowAnonymous]
//        [HttpPost]
//        public async Task<ActionResult> Inserir(CreateUsuarioDTO DTO)
//        {
//            try
//            {
//                if (!ModelState.IsValid) return CustomResponse(ModelState);
//                var result = await _service.InsertAsync<CreateUsuarioDTO, UsuarioDTO, UsuarioValidator>(DTO);
//                return CustomResponse(result);
//            }
//            catch (Exception ex)
//            {
//                NotificarErro(ex.Message);
//                return CustomResponse();
//            }
//        }

//        [AllowAnonymous]
//        [HttpPut]
//        public async Task<ActionResult> Atualizar(UsuarioDTO DTO)
//        {
//            try
//            {
//                if (!ModelState.IsValid) return CustomResponse(ModelState);
//                //var username = HttpContext.User.Identity.Name;
//                var result = _service.Update<UsuarioDTO, UsuarioDTO, UsuarioValidator>(DTO);
//                return CustomResponse(result);
//            }
//            catch (Exception ex)
//            {
//                NotificarErro(ex.Message);
//                return CustomResponse();
//            }
//        }
//    }
//}
