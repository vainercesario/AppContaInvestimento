using AutoMapper;
using Dominio.Interfaces.Repostorios;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W_CC.Aplicacao.DTO;
using W_CC.Aplicacao.Interfaces;
using W_CC.Dominio.Interfaces.Repostorios;
using W_CC.Dominio.Interfaces.Servicos;
using W_CC.Dominio.Model;
using W_CC.Infra.Data.Interfaces;

namespace W_CC.Aplicacao.Aplicacoes
{
    public class OperacoesApp : BaseApp, IOperacoesApp
    {
        private readonly IOperacoesRepositorio _operacaoRepositorio;
        private readonly IOperacoesServico _operacaoServico;

        private readonly IContasCorrentesRepositorio _contaRepositorio;

        private readonly IMapper _mapper;

        public OperacoesApp(IOperacoesRepositorio operacaoRepositorio, IOperacoesServico operacaoServico
            , IContasCorrentesRepositorio contaRepositorio
            , IMapper mapper, ITransacao trans)
            : base(trans)
        {
            _operacaoRepositorio = operacaoRepositorio;
            _operacaoServico = operacaoServico;
            _contaRepositorio = contaRepositorio;

            _mapper = mapper;
        }

        public OperacoesViewModel Depositar(OperacoesViewModel operacao)
        {
            operacao.ContaCorrenteId = _contaRepositorio.Listar().FirstOrDefault().Id;
            
            operacao = _mapper.Map<OperacoesViewModel>(
                _operacaoServico.Depositar(
                    _mapper.Map<Operacoes>(operacao)
                )
            );
            
            if (!operacao.Validacoes.Any())
                Commit();

            return operacao;
        }

        public IEnumerable<OperacoesViewModel> Listar()
        {
            return _mapper.Map<IEnumerable<OperacoesViewModel>>(_operacaoRepositorio.Listar().OrderByDescending(o => o.Data));
        }

        public OperacoesViewModel Pagar(OperacoesViewModel operacao)
        {
            operacao.ContaCorrenteId = _contaRepositorio.Listar().FirstOrDefault().Id;
            
            operacao = _mapper.Map<OperacoesViewModel>(
                _operacaoServico.Pagar(
                    _mapper.Map<Operacoes>(operacao)
                )
            );
            
            if (!operacao.Validacoes.Any())
                Commit();

            return operacao;
        }

        public OperacoesViewModel Resgatar(OperacoesViewModel operacao)
        {
            operacao.ContaCorrenteId = _contaRepositorio.Listar().FirstOrDefault().Id;
            
            operacao = _mapper.Map<OperacoesViewModel>(
                _operacaoServico.Resgatar(
                    _mapper.Map<Operacoes>(operacao)
                )
            );

            if (!operacao.Validacoes.Any())
                Commit();

            return operacao;
        }
    }
}
