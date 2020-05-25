using Dominio.Interfaces.Repostorios;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using W_CC.Dominio.Interfaces.Servicos;
using W_CC.Dominio.Model;
using W_CC.Dominio.Validacoes;
using W_CC.Dominio.Validacoes.Entidades;

namespace W_CC.Dominio.Servicos
{
    public class PessoasServico : IPessoaServico
    {
        private readonly IPessoasRepositorio _pessoasRepositorio;
        public PessoasServico(IPessoasRepositorio pessoasRepositorio)
        {
            _pessoasRepositorio = pessoasRepositorio;
        }

        public Pessoas Adicionar(Pessoas pessoa)
        {
            var validacao = new PessoasValidadas(_pessoasRepositorio);
            pessoa = validacao.Validar(ref pessoa);

            if (!pessoa.Validacoes.Any())
                pessoa = _pessoasRepositorio.Adicionar(pessoa);

            return pessoa;
        }

        public Pessoas Atualizar(Pessoas pessoa)
        {
            var validacao = new PessoasValidadas(_pessoasRepositorio);
            pessoa = validacao.Validar(ref pessoa);

            if (!pessoa.Validacoes.Any())
                pessoa = _pessoasRepositorio.Atualizar(pessoa);

            return pessoa;
        }

        public void Remover(Pessoas pessoa)
        {
            _pessoasRepositorio.Remover(pessoa);
        }
    
        
    }
}
