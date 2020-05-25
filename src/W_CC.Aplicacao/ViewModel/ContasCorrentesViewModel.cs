using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using W_CC.Aplicacao.DTO.Validacoes;

namespace W_CC.Aplicacao.DTO
{
    public class ContasCorrentesViewModel : ValidacaoViewModel
    {
        public ContasCorrentesViewModel()
        {
            Operacoes = new List<OperacoesViewModel>();
            Pessoa = new PessoasViewModel()
            {
                Id = Guid.NewGuid()
            };
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório.")]
        [DisplayName("Agência")]
        public int Agencia { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório.")]
        [DisplayName("Conta Corrente")]
        public int Conta { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório.")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Currency)]
        public decimal Saldo { get; set; }

        [DisplayName("Última Movimentação")]
        public DateTime UltimaMovimentacao { get; set; }
        [NotMapped]
        public Guid PessoaId { get; set; }
        [NotMapped]
        public virtual PessoasViewModel Pessoa { get; set; }
        [NotMapped]
        public virtual IEnumerable<OperacoesViewModel> Operacoes { get; set; }

    }
}
