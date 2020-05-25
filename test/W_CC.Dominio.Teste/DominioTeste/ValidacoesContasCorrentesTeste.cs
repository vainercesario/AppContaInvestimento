using Dominio.Interfaces.Repostorios;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using W_CC.Dominio.Interfaces.Servicos;
using W_CC.Dominio.Model;
using W_CC.Dominio.Servicos;
using W_CC.Dominio.Validacoes;
using W_CC.Infra.Data.Repositorios.Entidades;
using Xunit;

namespace W_CC.Dominio.Teste.DominioTeste
{
    public class ValidacoesContasCorrentesTeste
    {
        private Mock<IContasCorrentesRepositorio> mock;
        
        readonly ContasCorrentes ContaCorrente_AgenciaEContaComZero = new ContasCorrentes()
        {
            Id = Guid.NewGuid(),
            Agencia = 0,
            Conta = 0,
            UltimaMovimentacao = DateTime.Now,
            PessoaId = Guid.NewGuid(),
            Pessoa = new Pessoas()
            {
                Nome = "Vainer",
                CPF = "01473273005"
            }
        };

        readonly ContasCorrentes ContaCorrente_SemPessoaInformado = new ContasCorrentes()
        {
            Id = Guid.NewGuid(),
            Agencia = 123,
            Conta = 123,
            UltimaMovimentacao = DateTime.Now,
            PessoaId = Guid.NewGuid()
        };

        public ValidacoesContasCorrentesTeste()
        {
            mock = new Mock<IContasCorrentesRepositorio>(MockBehavior.Default);

            ContasCorrentes contaCorrenteComErro = new ContasCorrentes()
            {
                Agencia = 123,
                Conta = 123,
                UltimaMovimentacao = DateTime.Now,
                PessoaId = Guid.NewGuid()
            };
            contaCorrenteComErro.Validacoes.Add(new ItemValidacao()
            {
                NomePropriedade = "Agencia",
                Mensagem = "A Agência precisa conter um registro válido!"
            });
            contaCorrenteComErro.Validacoes.Add(new ItemValidacao()
            {
                NomePropriedade = "Conta",
                Mensagem = "A Conta precisa conter um registro válido!"
            });

            mock.Setup(cc => cc.Adicionar(ContaCorrente_AgenciaEContaComZero))
                .Returns(() => contaCorrenteComErro);


            ContasCorrentes contaCorrenteSemPessoa = new ContasCorrentes()
            {
                Agencia = 123,
                Conta = 123,
                UltimaMovimentacao = DateTime.Now,
                PessoaId = Guid.NewGuid()
            };
            contaCorrenteSemPessoa.Validacoes.Add(new ItemValidacao()
            {
                NomePropriedade = "Pessoa.Nome",
                Mensagem = "Uma Pessoa não foi vinculada à conta!"
            });
            
            mock.Setup(cc => cc.Adicionar(ContaCorrente_SemPessoaInformado))
                .Returns(() => contaCorrenteSemPessoa);

            
        }

        private List<ItemValidacao>? ValidacoesAdicionarFake(ContasCorrentes contasCorrentes)
        {
            ContasCorrentesServico contasCorrentesServico = new ContasCorrentesServico(mock.Object);

            var retorno = contasCorrentesServico.Adicionar(contasCorrentes);

            return retorno.Validacoes;
        }

        [Fact]
        public void TestarAdicionarContaComAgenciaEContaZerados()
        {
            List<ItemValidacao> validacoes = ValidacoesAdicionarFake(ContaCorrente_AgenciaEContaComZero);

            var validacaoEsperada = new List<ItemValidacao>();
            validacaoEsperada.Add(new ItemValidacao()
            {
                NomePropriedade = "Agencia",
                Mensagem = "A Agência precisa conter um registro válido!"
            });
            validacaoEsperada.Add(new ItemValidacao()
            {
                NomePropriedade = "Conta",
                Mensagem = "A Conta precisa conter um registro válido!"
            });

            validacoes.Should().BeEquivalentTo(validacaoEsperada, "Resultado incorreto na validação de Agencia e Conta informado com 0.");
        }

        [Fact]
        public void TestarAdicionarContaSemPessoa()
        {
            List<ItemValidacao> validacoes = ValidacoesAdicionarFake(ContaCorrente_SemPessoaInformado);

            var validacaoEsperada = new List<ItemValidacao>();
            validacaoEsperada.Add(new ItemValidacao()
            {
                NomePropriedade = "Pessoa.Nome",
                Mensagem = "Uma Pessoa não foi vinculada à conta!"
            });

            validacoes.Should().BeEquivalentTo(validacaoEsperada, "Resultado incorreto na validação de  Adicionar Conta Sem Pessoa.");
        }

        [Fact]
        public void TestarAdicionarContaSucesso()
        {
            var PessoaIdAux = Guid.NewGuid();
            var ContaIdAux = Guid.NewGuid();
            ContasCorrentes ContaCorrente_Valida = new ContasCorrentes()
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
            mock.Setup(cc => cc.Adicionar(ContaCorrente_Valida))
                .Returns(() => new ContasCorrentes());

            List<ItemValidacao>? validacoes = ValidacoesAdicionarFake(ContaCorrente_Valida);
            validacoes.Should().BeNullOrEmpty("As regras de validações não foram atendidas");
        }
    }
}
