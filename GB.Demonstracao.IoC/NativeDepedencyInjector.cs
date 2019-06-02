using GB.Demonstracao.Domain.Interfaces;
using GB.Demonstracao.Infra.Data.Repositories;
using GB.Demonstracao.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GB.Demonstracao.IoC
{
    public static class NativeDepedencyInjector
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IClienteService, ClienteService>();
            services.AddSingleton<IClienteRepository, ClienteRepository>();
            services.AddSingleton<ITarefaService, TarefaService>();
            services.AddSingleton<ITarefaRepository, TarefaRepository>();
        }
    }
}
