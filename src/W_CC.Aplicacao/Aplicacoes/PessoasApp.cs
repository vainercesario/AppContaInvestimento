using AutoMapper;
using Dominio.Interfaces.Repostorios;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using W_CC.Aplicacao.DTO;
using W_CC.Aplicacao.Interfaces;
using W_CC.Dominio.Interfaces.Servicos;
using W_CC.Dominio.Model;
using W_CC.Infra.Data.Interfaces;

namespace W_CC.Aplicacao.Aplicacoes
{
    public class PessoasApp : BaseApp, IPessoasApp
    {
        private readonly IPessoasRepositorio _pessoaRepositorio;
        private readonly IPessoaServico _pessoaServico;
        private readonly IMapper _mapper;

        public PessoasApp(IPessoasRepositorio pessoaRepositorio, IPessoaServico pessoaServico, IMapper mapper, ITransacao trans)
            : base(trans)
        {
            _pessoaRepositorio = pessoaRepositorio;
            _pessoaServico = pessoaServico;
            _mapper = mapper;
        }

        public PessoasViewModel Adicionar(PessoasViewModel obj)
        {
            var pessoaRetorno = _pessoaServico.Adicionar(_mapper.Map<Pessoas>(obj));
            
            if (!pessoaRetorno.Validacoes.Any())
                Commit();
            
            return _mapper.Map<PessoasViewModel>(pessoaRetorno);
        }

        public PessoasViewModel Atualizar(PessoasViewModel obj)
        {
            var pessoaRetorno = _pessoaServico.Atualizar(_mapper.Map<Pessoas>(obj));

            if (!pessoaRetorno.Validacoes.Any())
                Commit();

            return _mapper.Map<PessoasViewModel>(pessoaRetorno);
        }
        public void Remover(PessoasViewModel obj)
        {
            _pessoaRepositorio.Remover(_mapper.Map<Pessoas>(obj));
        }

        public IEnumerable<PessoasViewModel> Listar()
        {
            return _mapper.Map<IEnumerable<PessoasViewModel>>(_pessoaRepositorio.Listar());
        }

        public PessoasViewModel ObterPorId(Guid id)
        {
            return _mapper.Map<PessoasViewModel>(_pessoaRepositorio.ObterPorId(id));
        }
    }
}
