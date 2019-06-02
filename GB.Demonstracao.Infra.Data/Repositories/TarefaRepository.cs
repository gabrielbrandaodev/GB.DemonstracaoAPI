using Dapper;
using GB.Demonstracao.Domain.Entities;
using GB.Demonstracao.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GB.Demonstracao.Infra.Data.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private IConfiguration _configuracoes;
        private readonly string _conexao;

        public TarefaRepository(IConfiguration configuration)
        {
            _configuracoes = configuration;
            _conexao = _configuracoes.GetConnectionString("Demonstracao");
        }

        public async Task AdicionarAsync(Tarefa tarefa)
        {
            using (var db = new MySqlConnection(_conexao))
            {
                var sql =
                    @"INSERT INTO `demonstracao`.`tarefas` 
                        (`Id`, `NomeArquivo`, `DataInicio`, `Progresso`, `Status`) 
                      VALUES (@Id, @NomeArquivo, @DataInicio, @Progresso, @Status);";

                await db.ExecuteAsync(sql, tarefa);
            }
        }

        public async Task AtualizarAsync(Tarefa tarefa)
        {
            using (var db = new MySqlConnection(_conexao))
            {
                var sql =
                    @"UPDATE `demonstracao`.`tarefas` 
                      SET
                        `Progresso` = @Progresso,
                        `DataFim` = @DataFim,
                        `Status` = @Status
                      WHERE Id = @Id; ";

                await db.ExecuteAsync(sql, tarefa);
            }
        }

        public async Task<IEnumerable<Tarefa>> ListarUltimasTarefasAsync()
        {
            var sql =
                @"SELECT 
                        tarefa.Id          as Id,
                        tarefa.NomeArquivo as NomeArquivo,
                        tarefa.DataInicio  as DataInicio,
                        tarefa.DataFim     as DataFim,
                        tarefa.Progresso   as Progresso,
                        tarefa.Status      as Status
                      FROM tarefas tarefa 
                      WHERE tarefa.DataInicio > @DataReferencia 
                      ORDER BY tarefa.DataInicio DESC;";

            using (var db = new MySqlConnection(_conexao))
                return db.Query<Tarefa>(sql, new { DataReferencia = DateTime.Now.AddDays(-1).Date });
        }
    }
}
