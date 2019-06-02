using System;

namespace GB.Demonstracao.Domain.Entities
{
    public class Cliente
    {
        public long Telefone { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataInsercao { get; set; }
    }
}
