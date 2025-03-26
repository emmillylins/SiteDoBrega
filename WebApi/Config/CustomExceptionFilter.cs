using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Config
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is UnauthorizedAccessException)
            {
                context.Result = new JsonResult(new { message = "Você não está autorizado a acessar este recurso." })
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
            }
            else if (context.Exception is SecurityTokenExpiredException ||
                     context.Exception is SecurityTokenInvalidIssuerException)
            {
                context.Result = new JsonResult(new { message = "Seu token de autenticação expirou ou é inválido." })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
            else
            {
                context.Result = new JsonResult(new { message = "Ocorreu um erro inesperado. Tente novamente mais tarde." })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }

}
