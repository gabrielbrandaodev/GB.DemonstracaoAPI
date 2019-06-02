using GB.Demonstracao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GB.Demonstracao.Domain.Interfaces
{
    public interface IClienteRepository
    {
        IEnumerable<Cliente> ListarTodos();
        Task AdicionarAsync(Cliente cliente);
    }
}
