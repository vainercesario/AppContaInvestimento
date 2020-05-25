using System;
using System.Collections.Generic;
using System.Text;
using W_CC.Dominio.Model;

namespace W_CC.Dominio.Model
{
    public class Pessoas : EntidadeBase
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public virtual IEnumerable<ContasCorrentes> ContasCorrentes { get; set; }

    }
}
