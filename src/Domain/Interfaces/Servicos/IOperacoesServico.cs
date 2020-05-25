using System;
using System.Collections.Generic;
using System.Text;
using W_CC.Dominio.Model;

namespace W_CC.Dominio.Interfaces.Servicos
{
    public interface IOperacoesServico : IDisposable
    {
        Operacoes Pagar(Operacoes operacao);
        Operacoes Resgatar(Operacoes operacao);
        Operacoes Depositar(Operacoes operacao);
        void Rentabilizar(Guid contaCorrenteId, decimal saldo);

    }
}
