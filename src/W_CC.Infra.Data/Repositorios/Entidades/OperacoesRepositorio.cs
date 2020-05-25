using System;
using System.Collections.Generic;
using System.Text;
using W_CC.Dominio.Interfaces.Repostorios;
using W_CC.Dominio.Model;
using W_CC.Infra.Data.ContextoConfig;
using W_CC.Infra.Data.Repositorio;

namespace W_CC.Infra.Data.Repositorios.Entidades
{
    public class OperacoesRepositorio : RepositorioBase<Operacoes>, IOperacoesRepositorio
    {
        public OperacoesRepositorio(Contexto contexto)
            : base(contexto)
    {

    }
}
}
