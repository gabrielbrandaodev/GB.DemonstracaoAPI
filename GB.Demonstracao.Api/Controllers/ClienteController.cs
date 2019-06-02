using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GB.Demonstracao.Domain.Interfaces;
using GB.Demonstracao.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GB.Demonstracao.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
            => _clienteService = clienteService;

        [HttpGet]
        [Route("ListarTodos")]
        public async Task<IActionResult> ListarTodos()
        {
            try
            {
                var clientes = _clienteService.ListarTodos();

                if (clientes != null && clientes.Count() > 0)
                    return Ok(clientes);
                else
                    return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("ImportarArquivo")]
        public async Task<IActionResult> ImportarArquivo(IFormFile arquivo)
        {
            try
            {
                _clienteService.ImportarArquivoAsync(arquivo);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        
    }
}