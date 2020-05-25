using Dominio.Interfaces.Repostorios;
using System;
using System.Collections.Generic;
using System.Text;
using W_CC.Dominio.Model;

namespace W_CC.Dominio.Validacoes.Entidades
{
    public class PessoasValidadas
    {
        private readonly IPessoasRepositorio _pessoasRepositorio;
        public PessoasValidadas(IPessoasRepositorio pessoasRepositorio)
        {
            _pessoasRepositorio = pessoasRepositorio;
        }

        public Pessoas Validar(ref Pessoas pessoa)
        {
            VerifiacarSeNomeExiste(ref pessoa);
            VerifiacarSeCPFExiste(ref pessoa);

            return pessoa;
        }

        private Pessoas VerifiacarSeNomeExiste(ref Pessoas pessoa)
        {
            var nomeExiste = _pessoasRepositorio.NomeJaExiste(pessoa);

            if (nomeExiste)
                pessoa.Validacoes.Add(new ItemValidacao
                {
                    NomePropriedade = "Nome",
                    Mensagem = "Já existe uma pessoa cadastrado com este nome."
                });

            return pessoa;

        }

        private Pessoas VerifiacarSeCPFExiste(ref Pessoas pessoa)
        {
            var nomeExiste = _pessoasRepositorio.CPFJaExiste(pessoa);

            if (nomeExiste)
                pessoa.Validacoes.Add(new ItemValidacao
                {
                    NomePropriedade = "CPF",
                    Mensagem = "Já existe uma pessoa cadastrado com este CPF."
                });

            return pessoa;
        }
    }
}
