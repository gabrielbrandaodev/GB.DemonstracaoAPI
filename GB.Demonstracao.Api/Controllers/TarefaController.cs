using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GB.Demonstracao.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GB.Demonstracao.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaService _tarefaService;

        public TarefaController(ITarefaService tarefaService)
        {
            _tarefaService = tarefaService;
        }

        [HttpGet]
        [Route("ListarUltimasTarefas")]
        public async Task<IActionResult> ListarUltimasTarefas()
        {
            try
            {
                var tarefas = await _tarefaService.ListarUltimasTarefasAsync();

                if (tarefas != null && tarefas.Count() > 0)
                    return Ok(tarefas);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}