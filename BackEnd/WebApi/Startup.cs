using FluentValidation.AspNetCore;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using WebApi.Configuration;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // DependencyInjectionConfig
            services.ResolveDependencies();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Site do Brega",
                    Version = "v1",
                    Contact = new OpenApiContact() { Name = "Emmy Lins", Email = "emycmlins@gmail.com" },
                });

                // documentação do Swagger
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();

            services.AddDbContext<InfraDbContext>(options =>
            {
                options.UseSqlServer(@"data source=(localdb)\Db;initial catalog=DataBase;TrustServerCertificate=True;Integrated Security=SSPI", opt =>
                {
                    opt.MigrationsAssembly("WebApi");
                    opt.CommandTimeout(180);
                    opt.EnableRetryOnFailure(5);
                });
            });

            // configuração do AutoMapper
            services.AddAutoMapper(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // configuração do middleware do Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi");
            });

            app.UseRouting();
            app.UseAuthorization();

            app.UseCors(options => options.AllowAnyOrigin());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

}
