using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Infrastructure.Base;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Infrastructure.Notifications;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication;

namespace WebApi.Configuration
{
    public static class DependencyInjectionConfig
    {
        /// <summary>
        /// Serviço de injeção de dependência, injetar sempre que tiver criado um novo service e um novo repository. 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns> 
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<InfraDbContext>();
            services.AddScoped<AuthenticationService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<INotificador, Notificador>();

            services.AddScoped<IUsuarioService, UsuarioService<Usuario>>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            //permite personalizar a documentação do Swagger conforme necessário.
            //services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();                       

            return services;
        }
    }
}
