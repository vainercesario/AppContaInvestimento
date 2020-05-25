using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace W_CC.Dominio.Model
{
    public class Operacoes : EntidadeBase
    {
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public Guid ContaCorrenteId { get; set; }
        public TipoOperacao TipoOperacao { get; set; } 
        public virtual ContasCorrentes ContaCorrente { get; set; }
    }
}
