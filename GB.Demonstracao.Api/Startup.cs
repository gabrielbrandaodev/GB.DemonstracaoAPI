using GB.Demonstracao.Domain.Interfaces;
using GB.Demonstracao.Infra.Data.Repositories;
using GB.Demonstracao.IoC;
using GB.Demonstracao.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace GB.Demonstracao.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(opcoes =>
                {
                    opcoes.SerializerSettings.NullValueHandling =
                        Newtonsoft.Json.NullValueHandling.Ignore;
                });

            services.RegisterServices();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Projeto Demonstração",
                        Version = "v1",
                        Description = "Exemplo de uma aplicação com cadastro de clientes",
                        Contact = new Contact
                        {
                            Name = "Gabriel Brandão",
                            Email = "gabrielbrandaodev@gmail.com"
                        }
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "Projeto Demonstração");
            });
        }
    }
}
