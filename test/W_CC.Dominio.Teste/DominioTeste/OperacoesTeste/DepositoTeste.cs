using Dominio.Interfaces.Repostorios;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using W_CC.Dominio.Interfaces.Repostorios;
using W_CC.Dominio.Interfaces.Servicos;
using W_CC.Dominio.Model;
using W_CC.Dominio.Servicos;
using W_CC.Dominio.Validacoes;
using W_CC.Dominio.Validacoes.Entidades;
using Xunit;

namespace W_CC.Dominio.Teste.DominioTeste.OperacoesTeste
{
    public class DepositoTeste
    {
        private Mock<IOperacoesRepositorio> mock;
        private Mock<IContasCorrentesRepositorio> mockContaCorrente;
        private Mock<IOperacoesServico> mockService;

        Operacoes depositoNegativo = new Operacoes();
        Operacoes depositoPositivo = new Operacoes();

        ContasCorrentes ContaCorrente_Valida = new ContasCorrentes();

        public DepositoTeste()
        {
            mock = new Mock<IOperacoesRepositorio>(MockBehavior.Default);
            mockContaCorrente = new Mock<IContasCorrentesRepositorio>(MockBehavior.Strict);
            mockService = new Mock<IOperacoesServico>(MockBehavior.Strict);

            var PessoaIdAux = Guid.NewGuid();
            var ContaIdAux = Guid.NewGuid();
            ContaCorrente_Valida = new ContasCorrentes()
            {
                Id = ContaIdAux,
                Agencia = 123,
                Conta = 123,
                UltimaMovimentacao = DateTime.Now,
                PessoaId = PessoaIdAux,
                Pessoa = new Pessoas
                {
                    Id = PessoaIdAux,
                    Nome = "Vainer",
                    CPF = "1234"
                }
            };

            depositoNegativo = new Operacoes()
            {
                ContaCorrenteId = ContaIdAux,
                Data = DateTime.Now,
                Id = Guid.NewGuid(),
                TipoOperacao = TipoOperacao.Deposito,
                Valor = -1000
            };

            depositoPositivo = new Operacoes()
            {
                ContaCorrenteId = ContaIdAux,
                Data = DateTime.Now,
                Id = Guid.NewGuid(),
                TipoOperacao = TipoOperacao.Deposito,
                Valor = 1000
            };

        }

        private List<ItemValidacao>? ValidacaoDepositarFake(Operacoes operacao)
        {
            OperacoesServico operacaoServico = new OperacoesServico(mock.Object, mockContaCorrente.Object);

            var retorno = operacaoServico.Depositar(operacao);

            return retorno.Validacoes;
        }

        
        [Fact]
        public void TestarDepositoSomenteValorPositivo_Erro()
        {
            List<ItemValidacao> retorno = new List<ItemValidacao>();
            retorno.Add(new ItemValidacao()
            {
                NomePropriedade = "Valor",
                Mensagem = "Não é possível realizar a operação com valor informado!"
            });

            var validacoes = ValidacaoDepositarFake(depositoNegativo);
            validacoes.Should().BeEquivalentTo(retorno, "A validação de somente valor positivo de ERRO falhou.");
        }

        [Fact]
        public void TestarDepositoSomenteValorPositivo_Sucesso()
        {

            mockContaCorrente.Setup(cc => cc.ObterPorId(ContaCorrente_Valida.Id))
                .Returns(() => ContaCorrente_Valida);

            mockContaCorrente.Setup(cc => cc.Atualizar(ContaCorrente_Valida))
                .Returns(() => ContaCorrente_Valida);

            mock.Setup(cc => cc.Adicionar(depositoPositivo))
                .Returns(() => depositoPositivo);

            var validacoes = ValidacaoDepositarFake(depositoPositivo);
            validacoes.Should().BeNullOrEmpty("A validação de somente valor positivo de SUCESSO falhou.");
        }
    }
}
