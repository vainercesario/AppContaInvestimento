using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using W_CC.Aplicacao.DTO.Validacoes;

namespace W_CC.Aplicacao.DTO
{
    public class PessoasViewModel : ValidacaoViewModel
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MaxLength(120, ErrorMessage = "Informe no máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Informe no mínimo {0} caracteres")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [MaxLength(11, ErrorMessage = "Informe 11 dígitos")]
        [MinLength(11, ErrorMessage = "Informe 11 dígitos")]
        public string CPF { get; set; }

    }
}
