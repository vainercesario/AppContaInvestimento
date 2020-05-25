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
    public class ResgateValidoTeste
    {
        private Mock<IOperacoesRepositorio> mock;
        private Mock<IContasCorrentesRepositorio> mockContaCorrente;
        private Mock<IOperacoesServico> mockService;

        ContasCorrentes ContaCorrente_Valida = new ContasCorrentes();

        Operacoes resgateValorZerado = new Operacoes();
        Operacoes resgateValorNaoZerado = new Operacoes();
        Operacoes resgateValorNegativo = new Operacoes();
        Operacoes resgateValorPositivo = new Operacoes();
        Operacoes resgateSaldoInvalido = new Operacoes();
        Operacoes resgateSaldoValido = new Operacoes();
        Operacoes resgateValorPadraoInvalido = new Operacoes();
        Operacoes resgateValorPadraoValido = new Operacoes();

        public ResgateValidoTeste()
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

            resgateValorZerado = new Operacoes()
            {
                ContaCorrenteId = ContaIdAux,
                Data = DateTime.Now,
                Id = Guid.NewGuid(),
                TipoOperacao = TipoOperacao.Resgate,
                Valor = 0
            };
            resgateValorNaoZerado = new Operacoes()
            {
                ContaCorrenteId = ContaIdAux,
                Data = DateTime.Now,
                Id = Guid.NewGuid(),
                TipoOperacao = TipoOperacao.Resgate,
                Valor = 1000
            };
            resgateValorNegativo = new Operacoes()
            {
                ContaCorrenteId = ContaIdAux,
                Data = DateTime.Now,
                Id = Guid.NewGuid(),
                TipoOperacao = TipoOperacao.Resgate,
                Valor = -1000
            };
            resgateValorPositivo = new Operacoes()
            {
                ContaCorrenteId = ContaIdAux,
                Data = DateTime.Now,
                Id = Guid.NewGuid(),
                TipoOperacao = TipoOperacao.Resgate,
                Valor = 1000
            };
            resgateSaldoInvalido = new Operacoes()
            {
                ContaCorrenteId = ContaIdAux,
                Data = DateTime.Now,
                Id = Guid.NewGuid(),
                TipoOperacao = TipoOperacao.Resgate,
                Valor = 10000
            };
            resgateSaldoValido = new Operacoes()
            {
                ContaCorrenteId = ContaIdAux,
                Data = DateTime.Now,
                Id = Guid.NewGuid(),
                TipoOperacao = TipoOperacao.Resgate,
                Valor = 1000
            };
            resgateValorPadraoInvalido = new Operacoes()
            {
                ContaCorrenteId = ContaIdAux,
                Data = DateTime.Now,
                Id = Guid.NewGuid(),
                TipoOperacao = TipoOperacao.Resgate,
                Valor = 50000
            };
            resgateValorPadraoValido = new Operacoes()
            {
                ContaCorrenteId = ContaIdAux,
                Data = DateTime.Now,
                Id = Guid.NewGuid(),
                TipoOperacao = TipoOperacao.Resgate,
                Valor = 1000
            };

        }

        private List<ItemValidacao>? ValidacaoResgatarFake(Operacoes operacao)
        {
            OperacoesServico operacaoServico = new OperacoesServico(mock.Object, mockContaCorrente.Object);

            var retorno = operacaoServico.Resgatar(operacao);

            return retorno.Validacoes;
        }

        [Fact]
        public void TestarResgateSomenteValorPositivo_Erro()
        {
            List<ItemValidacao> retorno = new List<ItemValidacao>();
            retorno.Add(new ItemValidacao()
            {
                NomePropriedade = "Valor",
                Mensagem = "Não é possível realizar a operação com valor informado!"
            });

            var validacoes = ValidacaoResgatarFake(resgateValorNegativo);
            validacoes.Should().BeEquivalentTo(retorno, "A validação de somente valor positivo de ERRO falhou.");

            mock.Reset();
            mockContaCorrente.Reset();
            mockService.Reset();
        }

        [Fact]
        public void TestarResgateSomenteValorPositivo_Sucesso()
        {
            mockContaCorrente.Setup(cc => cc.ObterPorId(ContaCorrente_Valida.Id))
                .Returns(() => ContaCorrente_Valida);

            mockContaCorrente.Setup(cc => cc.Atualizar(ContaCorrente_Valida))
                .Returns(() => ContaCorrente_Valida);

            mock.Setup(cc => cc.Adicionar(resgateValorPositivo))
                .Returns(() => resgateValorPositivo);

            var validacoes = ValidacaoResgatarFake(resgateValorPositivo);
            validacoes.Should().BeNullOrEmpty("A validação de somente valor positivo de SUCESSO falhou.");

            mock.Reset();
            mockContaCorrente.Reset();
            mockService.Reset();
        }

        [Fact]
        public void TestarResgateNaoPodeSerZero_Erro()
        {
            List<ItemValidacao> retorno = new List<ItemValidacao>();
            retorno.Add(new ItemValidacao()
            {
                NomePropriedade = "Valor",
                Mensagem = "É preciso informar uma valor válido!"
            });

            var validacoes = ValidacaoResgatarFake(resgateValorZerado);
            validacoes.Should().BeEquivalentTo(retorno, "A validação de somente valor positivo de ERRO falhou.");

            mock.Reset();
            mockContaCorrente.Reset();
            mockService.Reset();
        }

        [Fact]
        public void TestarResgateNaoPodeSerZero_Sucesso()
        {
            mockContaCorrente.Setup(cc => cc.ObterPorId(ContaCorrente_Valida.Id))
                .Returns(() => ContaCorrente_Valida);

            mockContaCorrente.Setup(cc => cc.Atualizar(ContaCorrente_Valida))
                .Returns(() => ContaCorrente_Valida);

            mock.Setup(cc => cc.Adicionar(resgateValorNaoZerado))
                .Returns(() => resgateValorNaoZerado);

            var validacoes = ValidacaoResgatarFake(resgateValorNaoZerado);
            validacoes.Should().BeNullOrEmpty("A validação de somente valor positivo de SUCESSO falhou.");

            mock.Reset();
            mockContaCorrente.Reset();
            mockService.Reset();
        }

        [Fact]
        public void TestarResgateSaldoValido_Erro()
        {
            List<ItemValidacao> retorno = new List<ItemValidacao>();
            retorno.Add(new ItemValidacao()
            {
                NomePropriedade = "Valor",
                Mensagem = "O Valor informado ultrapassa o saldo disponível na conta."
            });

            var validacoes = ValidacaoResgatarFake(resgateSaldoInvalido);
            validacoes.Should().BeEquivalentTo(retorno, "A validação de Saldo Válido de ERRO falhou.");

            mock.Reset();
            mockContaCorrente.Reset();
            mockService.Reset();
        }

        [Fact]
        public void TestarResgateSaldoValido_Sucesso()
        {
            mockContaCorrente.Setup(cc => cc.ObterPorId(ContaCorrente_Valida.Id))
                .Returns(() => ContaCorrente_Valida);

            mockContaCorrente.Setup(cc => cc.Atualizar(ContaCorrente_Valida))
                .Returns(() => ContaCorrente_Valida);

            mock.Setup(cc => cc.Adicionar(resgateSaldoValido))
                .Returns(() => resgateSaldoValido);

            var validacoes = ValidacaoResgatarFake(resgateSaldoValido);
            validacoes.Should().BeNullOrEmpty("A validação de Saldo Válido de Sucesso falhou.");

            mock.Reset();
            mockContaCorrente.Reset();
            mockService.Reset();
        }

        [Fact]
        public void TestarResgateValorPadraoExcedido_Erro()
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
                Mensagem = "O Valor informado ultrapassa o limite máximo permitido para RESGATE."
            });

            var validacoes = ValidacaoResgatarFake(resgateValorPadraoInvalido);
            validacoes.Should().BeEquivalentTo(retorno, "A validação de Saldo Válido de ERRO falhou.");

            mock.Reset();
            mockContaCorrente.Reset();
            mockService.Reset();
        }

        [Fact]
        public void TestarResgateValorPadraoExcedido_Sucesso()
        {
            mockContaCorrente.Setup(cc => cc.ObterPorId(ContaCorrente_Valida.Id))
                .Returns(() => ContaCorrente_Valida);

            mockContaCorrente.Setup(cc => cc.Atualizar(ContaCorrente_Valida))
                .Returns(() => ContaCorrente_Valida);

            mock.Setup(cc => cc.Adicionar(resgateValorPadraoValido))
                .Returns(() => resgateValorPadraoValido);

            var validacoes = ValidacaoResgatarFake(resgateValorPadraoValido);
            validacoes.Should().BeNullOrEmpty("A validação de Saldo Válido de Sucesso falhou.");

            mock.Reset();
            mockContaCorrente.Reset();
            mockService.Reset();
        }
    }
}
