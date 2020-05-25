using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using W_CC.Aplicacao.DTO.Validacoes;

namespace W_CC.Aplicacao.DTO
{
    public class OperacoesViewModel : ValidacaoViewModel
    {
        public Guid Id { get; set; }
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório.")]
        public decimal Valor { get; set; }
        public TipoOperacao TipoOperacao { get; set; }
        public Guid ContaCorrenteId { get; set; }
        public virtual ContasCorrentesViewModel ContaCorrente { get; set; }
    }
}
