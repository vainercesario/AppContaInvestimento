using System;
using System.Collections.Generic;
using System.Text;
using W_CC.Aplicacao.DTO;

namespace W_CC.Aplicacao.Interfaces
{
    public interface IOperacoesApp// : IBaseApp<OperacoesViewModel>
    {
        IEnumerable<OperacoesViewModel> Listar();
        OperacoesViewModel Pagar(OperacoesViewModel operadores);
        OperacoesViewModel Resgatar(OperacoesViewModel operadores);
        OperacoesViewModel Depositar(OperacoesViewModel operadores);
    }
}
