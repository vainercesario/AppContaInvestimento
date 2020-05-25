using System;
using System.Collections.Generic;
using System.Text;

namespace W_CC.Aplicacao.DTO.Validacoes
{
    public abstract class ValidacaoViewModel
    {
        public ValidacaoViewModel()
        {
            Validacoes = new List<ItemValidacaoViewModel>();
        }

        public List<ItemValidacaoViewModel> Validacoes { get; set; }
    }
}
