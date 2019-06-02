using Dapper;
using GB.Demonstracao.Domain.Entities;
using GB.Demonstracao.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GB.Demonstracao.Infra.Data.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private IConfiguration _configuracoes;
        private readonly string _conexao;

        public ClienteRepository(IConfiguration configuration)
        {
            _configuracoes = configuration;
            _conexao = _configuracoes.GetConnectionString("Demonstracao");
        }

        public async Task AdicionarAsync(Cliente cliente)
        {
            using (var db = new MySqlConnection(_conexao))
            {
                var sql =
                    @"INSERT INTO `demonstracao`.`clientes` 
                                (`Telefone`, `Nome`, `DataNascimento`) 
                              VALUES (@Telefone, @Nome, @DataNascimento);";

                await db.ExecuteAsync(sql, cliente);
            }
        }

        public IEnumerable<Cliente> ListarTodos()
        {
            var sql =
                @"SELECT 
                        cliente.Telefone       as Telefone,
                        cliente.Nome           as Nome,
                        cliente.DataNascimento as DataNascimento,
                        cliente.DataInsercao   as DataInsercao
                      FROM clientes cliente; ";

            using (var db = new MySqlConnection(_conexao))
                return db.Query<Cliente>(sql);

        }
    }
}
