using GB.Demonstracao.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GB.Demonstracao.Domain.Interfaces
{
    public interface IClienteService
    {
        IEnumerable<Cliente> ListarTodos();
        Task ImportarArquivoAsync(IFormFile arquivo);
    }
}
