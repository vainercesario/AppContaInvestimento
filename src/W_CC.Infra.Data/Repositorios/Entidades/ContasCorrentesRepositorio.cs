using Dominio.Interfaces.Repostorios;
using W_CC.Dominio.Model;
using W_CC.Infra.Data.ContextoConfig;
using W_CC.Infra.Data.Repositorio;

namespace W_CC.Infra.Data.Repositorios.Entidades
{
    public class ContasCorrentesRepositorio : RepositorioBase<ContasCorrentes>, IContasCorrentesRepositorio
    {
        public ContasCorrentesRepositorio(Contexto context)
            : base(context)
        {

        }
    }
}
