using AutoMapper;
using Dominio.Interfaces.Repostorios;
using System;
using System.Collections.Generic;
using System.Linq;
using W_CC.Aplicacao.DTO;
using W_CC.Aplicacao.Interfaces;
using W_CC.Dominio.Interfaces.Repostorios;
using W_CC.Dominio.Interfaces.Servicos;
using W_CC.Dominio.Model;
using W_CC.Infra.Data.Interfaces;

namespace W_CC.Aplicacao.Aplicacoes
{
    public class ContasCorrentesApp : BaseApp, IContasCorrentesApp
    {
        private readonly IContasCorrentesServico _contasCorrentesServico;
        private readonly IContasCorrentesRepositorio _contasCorrentesRepositorio;
        private readonly IPessoasRepositorio _pessoaRepositorio;
        private readonly IOperacoesRepositorio _operacaoRepositorio;
        private readonly IMapper _mapper;

        public ContasCorrentesApp(IContasCorrentesServico contasCorrentesServico, IContasCorrentesRepositorio contasCorrentesRepositorio
            , IOperacoesRepositorio operacaoRepositorio, IPessoasRepositorio pessoaRepositorio
            , IMapper mapper, ITransacao trans)
            : base(trans)
        {
            _contasCorrentesServico = contasCorrentesServico;
            _contasCorrentesRepositorio = contasCorrentesRepositorio;
            _operacaoRepositorio = operacaoRepositorio;
            _pessoaRepositorio = pessoaRepositorio;
            _mapper = mapper;
        }

        public ContasCorrentesViewModel Adicionar(ContasCorrentesViewModel contaCorrente)
        {
            contaCorrente = _mapper.Map<ContasCorrentesViewModel>(
                _contasCorrentesServico.Adicionar(
                    _mapper.Map<ContasCorrentes>(contaCorrente)
                )
            );

            if (!contaCorrente.Validacoes.Any())
                Commit();


            return contaCorrente;
        }

        public ContasCorrentesViewModel Atualizar(ContasCorrentesViewModel contaCorrente)
        {
            contaCorrente = _mapper.Map<ContasCorrentesViewModel>(
                _contasCorrentesServico.Atualizar(
                    _mapper.Map<ContasCorrentes>(contaCorrente)
                )
            );

            if (!contaCorrente.Validacoes.Any())
                Commit();

            return contaCorrente;
        }

        public void Remover(ContasCorrentesViewModel obj)
        {
            _contasCorrentesServico.Remover(obj.Id);
        }

        public ContasCorrentesViewModel ObterConta()
        {
            var conta = _contasCorrentesRepositorio.Listar().FirstOrDefault();
            if (conta != null)
            {
                _contasCorrentesServico.CorrecaoFinanceira(conta);
                Commit();

                conta = _contasCorrentesRepositorio.Listar().FirstOrDefault();

                conta.Operacoes = _operacaoRepositorio.Listar().OrderByDescending(o => o.Data);
                conta.Pessoa = _pessoaRepositorio.ObterPorId(conta.PessoaId);
            }
                

            return _mapper.Map<ContasCorrentesViewModel>(conta);
        }

        public IEnumerable<ContasCorrentesViewModel> Listar()
        {
            return _mapper.Map<IEnumerable<ContasCorrentesViewModel>>(_contasCorrentesRepositorio.Listar());
        }

        public ContasCorrentesViewModel ObterPorId(Guid id)
        {
            return _mapper.Map<ContasCorrentesViewModel>(_contasCorrentesRepositorio.ObterPorId(id));
        }
    }
}
