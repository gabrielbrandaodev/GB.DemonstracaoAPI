using GB.Demonstracao.Domain;
using GB.Demonstracao.Domain.Entities;
using GB.Demonstracao.Domain.Enums;
using GB.Demonstracao.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GB.Demonstracao.Service
{
    public class TarefaService : ITarefaService
    {
        private ITarefaRepository _tarefaRepository;

        public TarefaService(ITarefaRepository tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }

        public async Task<Tarefa> IniciarTarefaAsync(string nomeArquivo)
        {
            var tarefa = new Tarefa()
            {
                Id = Guid.NewGuid().ToString(),
                NomeArquivo = nomeArquivo,
                DataInicio = DateTime.Now,
                Progresso = 0,
                Status = StatusTarefaEnum.NaoIniciada
            };

            await _tarefaRepository.AdicionarAsync(tarefa);

            return tarefa;
        }

        public async Task AtualizarProgressoAsync(Tarefa tarefa, double progresso)
        {
            tarefa.Progresso = progresso;
            tarefa.Status = tarefa.Status == StatusTarefaEnum.NaoIniciada ? StatusTarefaEnum.EmProcessamento : tarefa.Status;
            _tarefaRepository.AtualizarAsync(tarefa);
        }

        public async Task AtualizarStatusAsync(Tarefa tarefa, StatusTarefaEnum status)
        {
            tarefa.Status = status;
            tarefa.DataFim = status == StatusTarefaEnum.Erro ? DateTime.Now : tarefa.DataFim;
            _tarefaRepository.AtualizarAsync(tarefa);
        }

        public async Task FinalizarTarefaAsync(Tarefa tarefa)
        {
            tarefa.Status = StatusTarefaEnum.Finalizada;
            tarefa.DataFim = DateTime.Now;
            _tarefaRepository.AtualizarAsync(tarefa);
        }

        public Task<IEnumerable<Tarefa>> ListarUltimasTarefasAsync()
        {
            return _tarefaRepository.ListarUltimasTarefasAsync();
        }
    }
}
