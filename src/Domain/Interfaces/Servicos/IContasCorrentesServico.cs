using System;
using W_CC.Dominio.Model;

namespace W_CC.Dominio.Interfaces.Servicos
{
    public interface IContasCorrentesServico : IDisposable
    {
        ContasCorrentes Adicionar(ContasCorrentes contaCorrente);
        ContasCorrentes Atualizar(ContasCorrentes contaCorrente);
        void CorrecaoFinanceira(ContasCorrentes contaCorrente);
        void Remover(Guid id);
    }
}
