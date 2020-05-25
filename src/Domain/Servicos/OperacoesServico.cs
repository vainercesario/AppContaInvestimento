using Dominio.Interfaces.Repostorios;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W_CC.Dominio.Interfaces.Repostorios;
using W_CC.Dominio.Interfaces.Servicos;
using W_CC.Dominio.Model;
using W_CC.Dominio.Validacoes;
using W_CC.Dominio.Validacoes.Entidades;

namespace W_CC.Dominio.Servicos
{
    public class OperacoesServico : IOperacoesServico
    {
        private readonly IOperacoesRepositorio _operacoesRepositorio;
        private readonly IContasCorrentesRepositorio _contaRepositorio;

        public OperacoesServico(IOperacoesRepositorio operacoesRepositorio, IContasCorrentesRepositorio contaRepositorio)
        {
            _operacoesRepositorio = operacoesRepositorio;
            _contaRepositorio = contaRepositorio;
        }

        public Operacoes Depositar(Operacoes operacao)
        {
            var validacao = new OperacoesValidadas(_operacoesRepositorio, _contaRepositorio);
            operacao = validacao.DepositarValidado(ref operacao);

            if (!operacao.Validacoes.Any())
            {
                operacao.Data = DateTime.Now;
                
                if (operacao.ContaCorrenteId == Guid.Empty)
                    operacao.ContaCorrenteId = _contaRepositorio.Listar().FirstOrDefault().Id;

                operacao = _operacoesRepositorio.Adicionar(operacao);

                var conta = _contaRepositorio.ObterPorId(operacao.ContaCorrenteId);
                conta.Saldo += operacao.Valor;

                _contaRepositorio.Atualizar(conta);
            }

            return operacao;
        }

        public void Dispose()
        {
            _operacoesRepositorio.Dispose();
        }

        public Operacoes Pagar(Operacoes operacao)
        {
            var validacao = new OperacoesValidadas(_operacoesRepositorio, _contaRepositorio);
            operacao = validacao.PagarValidado(ref operacao);

            if (!operacao.Validacoes.Any())
            {
                operacao.Valor = operacao.Valor * (-1);
                operacao.Data = DateTime.Now;

                if (operacao.ContaCorrenteId == Guid.Empty)
                    operacao.ContaCorrenteId = _contaRepositorio.Listar().FirstOrDefault().Id;

                operacao = _operacoesRepositorio.Adicionar(operacao);

                var conta = _contaRepositorio.ObterPorId(operacao.ContaCorrenteId);
                conta.Saldo += operacao.Valor;

                _contaRepositorio.Atualizar(conta);
            }

            return operacao;
        }

        public void Rentabilizar(Guid contaCorrenteId, decimal saldo)
        {

            _operacoesRepositorio.Adicionar(new Operacoes()
            {
                Id = Guid.NewGuid(),
                ContaCorrenteId = contaCorrenteId,
                Data = DateTime.Now.Date,
                TipoOperacao = TipoOperacao.Rendimento,
                Valor = Math.Round(saldo, 2)
            });

        }

        public Operacoes Resgatar(Operacoes operacao)
        {
            var validacao = new OperacoesValidadas(_operacoesRepositorio, _contaRepositorio);
            operacao = validacao.ResgatarValidado(ref operacao);

            if (!operacao.Validacoes.Any())
            {
                operacao.Valor = operacao.Valor * (-1);
                operacao.Data = DateTime.Now;

                if (operacao.ContaCorrenteId == Guid.Empty)
                    operacao.ContaCorrenteId = _contaRepositorio.Listar().FirstOrDefault().Id;

                operacao = _operacoesRepositorio.Adicionar(operacao);

                var conta = _contaRepositorio.ObterPorId(operacao.ContaCorrenteId);
                conta.Saldo += operacao.Valor;

                _contaRepositorio.Atualizar(conta);
            }


            return operacao;
        }
    }
}
