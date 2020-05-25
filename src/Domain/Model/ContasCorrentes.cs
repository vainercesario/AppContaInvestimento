using System;
using System.Collections.Generic;

namespace W_CC.Dominio.Model
{
    public class ContasCorrentes : EntidadeBase
    {
        public ContasCorrentes()
        {
            Operacoes = new List<Operacoes>();
        }
        public int Agencia { get; set; }
        public int Conta { get; set; }
        public decimal Saldo { get; set; }
        public DateTime UltimaMovimentacao { get; set; }
        public Guid PessoaId { get; set; }
        public virtual Pessoas Pessoa { get; set; }
        public virtual IEnumerable<Operacoes> Operacoes { get; set; }
    }
}
