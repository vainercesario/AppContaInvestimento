using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace W_CC.Dominio.Validacoes
{
    public abstract class Validacao
    {
        public Validacao()
        {
            Validacoes = new List<ItemValidacao>();
        }

        [NotMapped]
        public List<ItemValidacao> Validacoes { get; set; }
    }
}
