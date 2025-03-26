using Application.Services;
using Infrastructure.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;

namespace WebApi.Config
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Middleware para validar tokens de autenticação e gerenciar sessões de usuários.
        /// Verifica JWT, valida informações de sessão e aplica políticas de acesso.
        /// </summary>
        /// <param name="context">O contexto da solicitação HTTP.</param>
        /// <param name="authService">O serviço de autenticação responsável pela manipulação de tokens.</param>
        /// <returns>Uma tarefa assíncrona que representa a execução do middleware.</returns>
        public async Task Invoke(HttpContext context, AuthService authService, DataDbContext dbContext)
        {
            DateTime horaAtualBrasilia = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));

            var tokenAcesso = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            var rotaApi = context.Request.Path.StartsWithSegments(new PathString("/api"));

            if (rotaApi)
            {
                var endpoint = context.GetEndpoint();

                if (endpoint == null)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    context.Response.ContentType = "application/json";

                    var response = new
                    {
                        success = false,
                        errors = new[] { "Rota não encontrada." }
                    };

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    }));
                    return;
                }

                var requestedPath = context.Request.Path.ToString();

                if (requestedPath is not null)
                {
                    if (requestedPath.Equals("/api/auth/login", StringComparison.OrdinalIgnoreCase)
                        || requestedPath.Equals("/api/auth/logout", StringComparison.OrdinalIgnoreCase)
                        || requestedPath.Equals("/api/auth/cadastro", StringComparison.OrdinalIgnoreCase))
                    {
                        await _next(context);
                        return;
                    }
                }

                //if (authService.AllowsAnonymousRoute(endpoint))
                //{
                //    await _next(context);
                //    return;
                //}

                try
                {
                    if (string.IsNullOrEmpty(tokenAcesso) || tokenAcesso == "Bearer")
                    {
                        throw new SecurityTokenValidationException("Acesso negado. Faça o login para acessar.");
                    }

                    var token = await dbContext.Tokens.FirstOrDefaultAsync(t => t.Value == tokenAcesso);

                    if (token is null || token.IsExpired)
                    {
                        throw new SecurityTokenValidationException("Token expirado ou inválido");
                    }
                }
                catch (SecurityTokenValidationException se)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    context.Response.ContentType = "application/json";

                    var response = new
                    {
                        success = false,
                        errors = new[] { se.Message }
                    };

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    }));
                    return;
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var response = new
                    {
                        success = false,
                        errors = new[] { "Ocorreu um erro inesperado.", ex.Message }
                    };

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    }));
                    return;
                }
            }
            await _next(context);
        }
    }
}
