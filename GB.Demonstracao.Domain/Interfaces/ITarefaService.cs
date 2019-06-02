using GB.Demonstracao.Domain.Entities;
using GB.Demonstracao.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GB.Demonstracao.Domain.Interfaces
{
    public interface ITarefaService
    {
        Task<Tarefa> IniciarTarefaAsync(string nomeArquivo);
        Task AtualizarProgressoAsync(Tarefa tarefa, double progresso);
        Task AtualizarStatusAsync(Tarefa tarefa, StatusTarefaEnum status);
        Task FinalizarTarefaAsync(Tarefa tarefa);
        Task<IEnumerable<Tarefa>> ListarUltimasTarefasAsync();
    }
}
