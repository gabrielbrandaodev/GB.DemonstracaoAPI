using GB.Demonstracao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GB.Demonstracao.Domain.Interfaces
{
    public interface ITarefaRepository
    {
        Task AdicionarAsync(Tarefa tarefa);
        Task AtualizarAsync(Tarefa tarefa);
        Task<IEnumerable<Tarefa>> ListarUltimasTarefasAsync();
    }
}
