using Application.Interfaces;
using Application.Services;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Infrastructure.Notifications;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication;

namespace WebApi.Config
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
            services.AddScoped<DataDbContext>();
            services.AddScoped<AuthenticationService>();
            services.AddScoped<INotificador, Notificador>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<AuthService>();


            //permite personalizar a documentação do Swagger conforme necessário.
            //services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();                       

            return services;
        }
    }
}
