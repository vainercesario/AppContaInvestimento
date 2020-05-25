using Dominio.Interfaces.Repostorios;
using Org.BouncyCastle.Asn1.Esf;
using System;
using System.Linq;
using W_CC.Dominio.Interfaces.Repostorios;
using W_CC.Dominio.Interfaces.Servicos;
using W_CC.Dominio.Model;
using W_CC.Dominio.Validacoes.Entidades;

namespace W_CC.Dominio.Servicos
{
    public class ContasCorrentesServico : IContasCorrentesServico
    {
        private readonly IContasCorrentesRepositorio _contaCorrenteRepositorio;
        private readonly IOperacoesServico _operacoesServico;

        private const double taxaJurosCDIDia = 0.0517 / 253;

        public ContasCorrentesServico(IContasCorrentesRepositorio contaCorrenteRepositorio, IOperacoesServico operacoesServico)
        {
            _contaCorrenteRepositorio = contaCorrenteRepositorio;
            _operacoesServico = operacoesServico;
        }

        public ContasCorrentes Adicionar(ContasCorrentes contaCorrente)
        {
            var validacao = new ContasCorrentesValidadas(_contaCorrenteRepositorio);
            contaCorrente = validacao.ContaValidada(ref contaCorrente);

            if (!contaCorrente.Validacoes.Any())
            {
                contaCorrente.UltimaMovimentacao = DateTime.Now;
                contaCorrente.PessoaId = contaCorrente.Pessoa.Id;
                contaCorrente = _contaCorrenteRepositorio.Adicionar(contaCorrente);
            }

            return contaCorrente;
        }

        public ContasCorrentes Atualizar(ContasCorrentes contaCorrente)
        {
            var validacao = new ContasCorrentesValidadas(_contaCorrenteRepositorio);
            contaCorrente = validacao.ContaValidada(ref contaCorrente);

            if (!contaCorrente.Validacoes.Any())
            {
                contaCorrente.UltimaMovimentacao = DateTime.Now;
                contaCorrente = _contaCorrenteRepositorio.Atualizar(contaCorrente);
            }

            return contaCorrente;
        }

        public void CorrecaoFinanceira(ContasCorrentes contaCorrente)
        {
            if (contaCorrente.UltimaMovimentacao.Date == DateTime.Now.Date || contaCorrente.Saldo == 0)
                return;

            var diasUteisPeriodo = GetDiferencaDias(contaCorrente.UltimaMovimentacao.Date, DateTime.Now.Date);

            if (diasUteisPeriodo == 0)
                return;

            var taxaJrs = Math.Round(taxaJurosCDIDia, 4);
            var baseDeCalculo = Math.Pow(1 + taxaJrs, diasUteisPeriodo) - 1;
            var saldoAtualizado = contaCorrente.Saldo * Convert.ToDecimal(baseDeCalculo);

            contaCorrente.Saldo = contaCorrente.Saldo + saldoAtualizado;
            contaCorrente.UltimaMovimentacao = DateTime.Now;
            _contaCorrenteRepositorio.Atualizar(contaCorrente);

            _operacoesServico.Rentabilizar(contaCorrente.Id, saldoAtualizado);

            return;
        }

        public void Dispose()
        {
            _contaCorrenteRepositorio.Dispose();
        }

        public void Remover(Guid id)
        {
            throw new NotImplementedException();
        }

        private static int GetDiferencaDias(DateTime initialDate, DateTime finalDate)
        {
            var dias = 0;
            var diasCount = 0;
            dias = initialDate.Subtract(finalDate).Days;

            if (dias < 0)
                dias = dias * -1;

            for (int i = 1; i <= dias; i++)
            {
                initialDate = initialDate.AddDays(1);

                if (initialDate.DayOfWeek != DayOfWeek.Sunday &&
                    initialDate.DayOfWeek != DayOfWeek.Saturday)
                    diasCount++;
            }
            return diasCount;
        }
    }
}
