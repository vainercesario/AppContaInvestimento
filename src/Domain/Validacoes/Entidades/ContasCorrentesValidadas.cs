using Dominio.Interfaces.Repostorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W_CC.Dominio.Model;

namespace W_CC.Dominio.Validacoes.Entidades
{
    public class ContasCorrentesValidadas
    {
        private readonly IContasCorrentesRepositorio _contaRepositorio;

        public ContasCorrentesValidadas(IContasCorrentesRepositorio contaRepositorio)
        {
            _contaRepositorio = contaRepositorio;
        }

        public virtual ContasCorrentes ContaValidada(ref ContasCorrentes contaCorrente)
        {
            NenhumaContaAberta(ref contaCorrente);
            ObrigatorioPessoaVinculada(ref contaCorrente);
            AgenciaNaoPodeSerZero(ref contaCorrente);
            ContaNaoPodeSerZero(ref contaCorrente);

            return contaCorrente;
        }

        private ContasCorrentes NenhumaContaAberta(ref ContasCorrentes contaCorrente)
        {
            var ExisteRegistro = _contaRepositorio.Listar().Count() > 0;

            if (ExisteRegistro)
                contaCorrente.Validacoes.Add(new ItemValidacao()
                {
                    NomePropriedade = "Conta",
                    Mensagem = "Já existe Conta aberta!"
                });

            return contaCorrente;
        }

        private ContasCorrentes ObrigatorioPessoaVinculada(ref ContasCorrentes contaCorrente)
        {
            var ExistePessoa = contaCorrente.Pessoa != null;

            if (!ExistePessoa)
                contaCorrente.Validacoes.Add(new ItemValidacao()
                {
                    NomePropriedade = "Pessoa.Nome",
                    Mensagem = "Uma Pessoa não foi vinculada à conta!"
                });

            return contaCorrente;
        }

        private ContasCorrentes AgenciaNaoPodeSerZero(ref ContasCorrentes contaCorrente)
        {
            var AgenciaZero = contaCorrente.Agencia == 0;

            if (AgenciaZero)
                contaCorrente.Validacoes.Add(new ItemValidacao()
                {
                    NomePropriedade = "Agencia",
                    Mensagem = "A Agência precisa conter um registro válido!"
                });

            return contaCorrente;
        }

        private ContasCorrentes ContaNaoPodeSerZero(ref ContasCorrentes contaCorrente)
        {
            var ContaZero = contaCorrente.Conta == 0;

            if (ContaZero)
                contaCorrente.Validacoes.Add(new ItemValidacao()
                {
                    NomePropriedade = "Conta",
                    Mensagem = "A Conta precisa conter um registro válido!"
                });

            return contaCorrente;
        }
    }
}
