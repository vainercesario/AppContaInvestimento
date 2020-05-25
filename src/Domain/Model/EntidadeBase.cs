using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using W_CC.Dominio.Validacoes;

namespace W_CC.Dominio.Model
{
    public abstract class EntidadeBase : Validacao
    {
        public Guid Id { get; set; }
    }
}
