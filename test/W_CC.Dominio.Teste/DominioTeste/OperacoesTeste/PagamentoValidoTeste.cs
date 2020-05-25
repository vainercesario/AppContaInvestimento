using Dominio.Interfaces.Repostorios;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using W_CC.Dominio.Interfaces.Repostorios;
using W_CC.Dominio.Interfaces.Servicos;
using W_CC.Dominio.Model;
using W_CC.Dominio.Servicos;
using W_CC.Dominio.Validacoes;
using W_CC.Dominio.Validacoes.Entidades;
using Xunit;

namespace W_CC.Dominio.Teste.DominioTeste.OperacoesTeste
{
    public class PagamentoValidoTeste
    {
        private Mock<IOperacoesRepositorio> mock;
        private Mock<IContasCorrentesRepositorio> mockContaCorrente;
        private Mock<IOperacoesServico> mockService;

        ContasCorrentes ContaCorrente_Valida = new ContasCorrentes();

        Operacoes pagamentoValorZerado = new Operacoes();
        Operacoes pagamentoValorNaoZerado = new Operacoes();
        Operacoes pagamentoValorNegativo = new Operacoes();
        Operacoes pagamentoValorPositivo = new Operacoes();
        Operacoes pagamentoSaldoInvalido = new Operacoes();
        Operacoes pagamentoSaldoValido = new Operacoes();
        Operacoes pagamentoValorPadraoInvalido = new Operacoes();
        Operacoes pagamentoValorPadraoValido = new Operacoes();

        public PagamentoValidoTeste()
        {
            mock = new Mock<IOperacoesRepositorio>(MockBehavior.Default);
            mockContaCorrente = new Mock<IContasCorrentesRepositorio>(MockBehavior.Default);
            mockService = new Mock<IOperacoesServico>(MockBehavior.Strict);

            var ContaIdAux = Guid.NewGuid();
            var PessoaIdAux = Guid.NewGuid();
            ContaCorrente_Valida = new ContasCorrentes()
            {
                Id = ContaIdAux,
                Agencia = 123,
                Conta = 123,
                UltimaMovimentacao = DateTime.Now,
                Saldo = 5000,
                PessoaId = PessoaIdAux,
                Pessoa = new Pessoas
                {
                    Id = PessoaIdAux,
                    Nome = "Vainer",
                    CPF = "1234"
                }
            };

            mockContaCorrente.Setup(cc => cc.ObterPorId(ContaIdAux))
                .Returns(() => ContaCorrente_Valida);

            pagamentoValorZerado = new Operacoes()
            {
                ContaCorrenteId = ContaIdAux,
                Data = DateTime.Now,
                Id = Guid.NewGuid(),
                TipoOperacao = TipoOperacao.Pagamento,
                Valor = 0
            };
            pagamentoValorNaoZerado = new Operacoes()
            {
                ContaCorrenteId = ContaIdAux,
                Data = DateTime.Now,
                Id = Guid.NewGuid(),
                TipoOperacao = TipoOperacao.Pagamento,
                Valor = 1000
            };
            pagamentoValorNegativo = new Operacoes()
            {
                ContaCorrenteId = ContaIdAux,
                Data = DateTime.Now,
                Id = Guid.NewGuid(),
                TipoOperacao = TipoOperacao.Pagamento,
                Valor = -1000
            };
            pagamentoValorPositivo = new Operacoes()
            {
                ContaCorrenteId = ContaIdAux,
                Data = DateTime.Now,
                Id = Guid.NewGuid(),
                TipoOperacao = TipoOperacao.Pagamento,
                Valor = 1000
            };
            pagamentoSaldoInvalido = new Operacoes()
            {
                ContaCorrenteId = ContaIdAux,
                Data = DateTime.Now,
                Id = Guid.NewGuid(),
                TipoOperacao = TipoOperacao.Pagamento,
                Valor = 10000
            };
            pagamentoSaldoValido = new Operacoes()
            {
                ContaCorrenteId = ContaIdAux,
                Data = DateTime.Now,
                Id = Guid.NewGuid(),
                TipoOperacao = TipoOperacao.Pagamento,
                Valor = 1000
            };
            pagamentoValorPadraoInvalido = new Operacoes()
            {
                ContaCorrenteId = ContaIdAux,
                Data = DateTime.Now,
                Id = Guid.NewGuid(),
                TipoOperacao = TipoOperacao.Pagamento,
                Valor = 30000
            };
            pagamentoValorPadraoValido = new Operacoes()
            {
                ContaCorrenteId = ContaIdAux,
                Data = DateTime.Now,
                Id = Guid.NewGuid(),
                TipoOperacao = TipoOperacao.Pagamento,
                Valor = 1000
            };

        }

        private List<ItemValidacao>? ValidacaoPagarFake(Operacoes operacao)
        {
            OperacoesServico operacaoServico = new OperacoesServico(mock.Object, mockContaCorrente.Object);

            var retorno = operacaoServico.Pagar(operacao);
            
            return retorno.Validacoes;
        }

        [Fact]
        public void TestarPagamentoSomenteValorPositivo_Erro()
        {
            List<ItemValidacao> retorno = new List<ItemValidacao>();
            retorno.Add(new ItemValidacao()
            {
                NomePropriedade = "Valor",
                Mensagem = "Não é possível realizar a operação com valor informado!"
            });

            var validacoes = ValidacaoPagarFake(pagamentoValorNegativo);
            validacoes.Should().BeEquivalentTo(retorno, "A validação de somente valor positivo de ERRO falhou.");

            mock.Reset();
            mockContaCorrente.Reset();
            mockService.Reset();
        }

        [Fact]
        public void TestarPagamentoSomenteValorPositivo_Sucesso()
        {
            mockContaCorrente.Setup(cc => cc.ObterPorId(ContaCorrente_Valida.Id))
                .Returns(() => ContaCorrente_Valida);

            mockContaCorrente.Setup(cc => cc.Atualizar(ContaCorrente_Valida))
                .Returns(() => ContaCorrente_Valida);

            mock.Setup(cc => cc.Adicionar(pagamentoValorPositivo))
                .Returns(() => pagamentoValorPositivo);

            var validacoes = ValidacaoPagarFake(pagamentoValorPositivo);
            validacoes.Should().BeNullOrEmpty("A validação de somente valor positivo de SUCESSO falhou.");

            mock.Reset();
            mockContaCorrente.Reset();
            mockService.Reset();
        }

        [Fact]
        public void TestarPagamentoNaoPodeSerZero_Erro()
        {
            List<ItemValidacao> retorno = new List<ItemValidacao>();
            retorno.Add(new ItemValidacao()
            {
                NomePropriedade = "Valor",
                Mensagem = "É preciso informar uma valor válido!"
            });

            var validacoes = ValidacaoPagarFake(pagamentoValorZerado);
            validacoes.Should().BeEquivalentTo(retorno, "A validação de somente valor positivo de ERRO falhou.");

            mock.Reset();
            mockContaCorrente.Reset();
            mockService.Reset();
        }

        [Fact]
        public void TestarPagamentoNaoPodeSerZero_Sucesso()
        {
            mockContaCorrente.Setup(cc => cc.ObterPorId(ContaCorrente_Valida.Id))
                .Returns(() => ContaCorrente_Valida);

            mockContaCorrente.Setup(cc => cc.Atualizar(ContaCorrente_Valida))
                .Returns(() => ContaCorrente_Valida);

            mock.Setup(cc => cc.Adicionar(pagamentoValorNaoZerado))
                .Returns(() => pagamentoValorNaoZerado);

            var validacoes = ValidacaoPagarFake(pagamentoValorNaoZerado);
            validacoes.Should().BeNullOrEmpty("A validação de somente valor positivo de SUCESSO falhou.");

            mock.Reset();
            mockContaCorrente.Reset();
            mockService.Reset();
        }

        [Fact]
        public void TestarPagamentoSaldoValido_Erro()
        {
            List<ItemValidacao> retorno = new List<ItemValidacao>();
            retorno.Add(new ItemValidacao()
            {
                NomePropriedade = "Valor",
                Mensagem = "O Valor informado ultrapassa o saldo disponível na conta."
            });

            var validacoes = ValidacaoPagarFake(pagamentoSaldoInvalido);
            validacoes.Should().BeEquivalentTo(retorno, "A validação de Saldo Válido de ERRO falhou.");

            mock.Reset();
            mockContaCorrente.Reset();
            mockService.Reset();
        }

        [Fact]
        public void TestarPagamentoSaldoValido_Sucesso()
        {
            mockContaCorrente.Setup(cc => cc.ObterPorId(ContaCorrente_Valida.Id))
                .Returns(() => ContaCorrente_Valida);

            mockContaCorrente.Setup(cc => cc.Atualizar(ContaCorrente_Valida))
                .Returns(() => ContaCorrente_Valida);

            mock.Setup(cc => cc.Adicionar(pagamentoSaldoValido))
                .Returns(() => pagamentoSaldoValido);

            var validacoes = ValidacaoPagarFake(pagamentoSaldoValido);
            validacoes.Should().BeNullOrEmpty("A validação de Saldo Válido de Sucesso falhou.");

            mock.Reset();
            mockContaCorrente.Reset();
            mockService.Reset();
        }

        [Fact]
        public void TestarPagamentoValorPadraoExcedido_Erro()
        {
            List<ItemValidacao> retorno = new List<ItemValidacao>();
            retorno.Add(new ItemValidacao()
            {
                NomePropriedade = "Valor",
                Mensagem = "O Valor informado ultrapassa o saldo disponível na conta."
            });
            retorno.Add(new ItemValidacao()
            {
                NomePropriedade = "Valor",
                Mensagem = "O Valor informado ultrapassa o máximo permitido para PAGAMENTO."
            });

            var validacoes = ValidacaoPagarFake(pagamentoValorPadraoInvalido);
            validacoes.Should().BeEquivalentTo(retorno, "A validação de Saldo Válido de ERRO falhou.");

            mock.Reset();
            mockContaCorrente.Reset();
            mockService.Reset();
        }

        [Fact]
        public void TestarPagamentoValorPadraoExcedido_Sucesso()
        {
            mockContaCorrente.Setup(cc => cc.ObterPorId(ContaCorrente_Valida.Id))
                .Returns(() => ContaCorrente_Valida);

            mockContaCorrente.Setup(cc => cc.Atualizar(ContaCorrente_Valida))
                .Returns(() => ContaCorrente_Valida);

            mock.Setup(cc => cc.Adicionar(pagamentoValorPadraoValido))
                .Returns(() => pagamentoValorPadraoValido);

            var validacoes = ValidacaoPagarFake(pagamentoValorPadraoValido);
            validacoes.Should().BeNullOrEmpty("A validação de Saldo Válido de Sucesso falhou.");

            mock.Reset();
            mockContaCorrente.Reset();
            mockService.Reset();
        }
    }
}
