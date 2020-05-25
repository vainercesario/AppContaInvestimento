using Dominio.Interfaces.Repostorios;
using System;
using System.Collections.Generic;
using System.Text;
using W_CC.Dominio.Interfaces.Repostorios;
using W_CC.Dominio.Model;

namespace W_CC.Dominio.Validacoes.Entidades
{
    public class OperacoesValidadas
    {
        private readonly IOperacoesRepositorio _operacoesRepositorio;
        private readonly IContasCorrentesRepositorio _contaRepositorio;

        public OperacoesValidadas(IOperacoesRepositorio operacoesRepositorio, IContasCorrentesRepositorio contaRepositorio)
        {
            _operacoesRepositorio = operacoesRepositorio;
            _contaRepositorio = contaRepositorio;
        }
        public virtual Operacoes DepositarValidado(ref Operacoes operacao)
        {
            SomenteValorPositivo(ref operacao);

            return operacao;
        }

        public virtual Operacoes PagarValidado(ref Operacoes operacao)
        {
            ValorNaoPodeSerZero(ref operacao);
            SaldoValido(ref operacao);
            ValorExcedeValorPadraoDePagamento(ref operacao);
            SomenteValorPositivo(ref operacao);

            return operacao;
        }

        public virtual Operacoes ResgatarValidado(ref Operacoes operacao)
        {
            ValorNaoPodeSerZero(ref operacao);
            SaldoValido(ref operacao);
            ValorExcedeValorPadraoParaResgate(ref operacao);
            SomenteValorPositivo(ref operacao);

            return operacao;
        }

        private Operacoes ValorNaoPodeSerZero(ref Operacoes operacao)
        {
            var valorZero = operacao.Valor.Equals(0);

            if (valorZero)
                operacao.Validacoes.Add(new ItemValidacao()
                {
                    NomePropriedade = "Valor",
                    Mensagem = "É preciso informar uma valor válido!"
                });

            return operacao;

        }

        private Operacoes SomenteValorPositivo(ref Operacoes operacao)
        {
            var valorNegativo = operacao.Valor < 0;

            if(valorNegativo)
                operacao.Validacoes.Add(new ItemValidacao()
                {
                    NomePropriedade = "Valor",
                    Mensagem = "Não é possível realizar a operação com valor informado!"
                });

            return operacao;
        }

        private Operacoes SaldoValido(ref Operacoes operacoes)
        {
            if ((_contaRepositorio.ObterPorId(operacoes.ContaCorrenteId).Saldo - operacoes.Valor) < 0)
                operacoes.Validacoes.Add(new ItemValidacao()
                {
                    NomePropriedade = "Valor",
                    Mensagem = "O Valor informado ultrapassa o saldo disponível na conta."
                });

            return operacoes;
        }

        private Operacoes ValorExcedeValorPadraoDePagamento(ref Operacoes operacoes)
        {
            if (operacoes.Valor > 10000)
                operacoes.Validacoes.Add(new ItemValidacao()
                {
                    NomePropriedade = "Valor",
                    Mensagem = "O Valor informado ultrapassa o máximo permitido para PAGAMENTO."
                });

            return operacoes;
        }

        private Operacoes ValorExcedeValorPadraoParaResgate(ref Operacoes operacoes)
        {
            if (operacoes.Valor > 30000)
                operacoes.Validacoes.Add(new ItemValidacao()
                {
                    NomePropriedade = "Valor",
                    Mensagem = "O Valor informado ultrapassa o limite máximo permitido para RESGATE."
                });

            return operacoes;
        }
    }
}
