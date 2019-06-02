using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GB.Demonstracao.Domain;
using GB.Demonstracao.Domain.Entities;
using GB.Demonstracao.Domain.Enums;
using GB.Demonstracao.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace GB.Demonstracao.Service
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repositorio;
        private readonly ITarefaService _tarefaService;

        public ClienteService(IClienteRepository repositorio, ITarefaService tarefaService)
        {
            _repositorio = repositorio;
            _tarefaService = tarefaService;
        }

        public async Task ImportarArquivoAsync(IFormFile arquivo)
        {
            double progresso = 0;
            int clientesImportados = 0;
            int clientesTotal = 0;
            Tarefa tarefa = null;

            try
            {
                IList<Cliente> clientes = new List<Cliente>();

                using (var reader = new StreamReader(arquivo.OpenReadStream()))
                {
                    var data = reader.ReadToEnd();
                    var linhas = data.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                    clientesTotal = linhas.Count();

                    tarefa = await _tarefaService.IniciarTarefaAsync(arquivo.FileName);

                    foreach (var linha in linhas)
                    {
                        progresso = (clientesImportados * 100 / clientesTotal);

                        var clienteLinha = linha.Split(',');
                        var cliente = new Cliente()
                        {
                            Telefone = long.Parse(clienteLinha[0]),
                            Nome = clienteLinha[1],
                            DataNascimento = DateTime.Parse(clienteLinha[2])
                        };

                        await _repositorio.AdicionarAsync(cliente);
                        await _tarefaService.AtualizarProgressoAsync(tarefa, progresso);

                        clientesImportados++;
                    }

                    _tarefaService.FinalizarTarefaAsync(tarefa);
                }
            }
            catch (Exception ex)
            {
                _tarefaService.AtualizarStatusAsync(tarefa, StatusTarefaEnum.Erro);
            }
        }

        public IEnumerable<Cliente> ListarTodos()
        {
            return _repositorio.ListarTodos();
        }
    }
}
