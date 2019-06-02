using GB.Demonstracao.Domain.Enums;
using System;

namespace GB.Demonstracao.Domain.Entities
{
    public class Tarefa
    {
        public string Id { get; set; }
        public string NomeArquivo { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public double Progresso { get; set; }
        public StatusTarefaEnum Status { get; set; }
    }
}
