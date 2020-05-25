using Dominio.Interfaces.Repostorios;
using System.Linq;
using W_CC.Dominio.Model;
using W_CC.Infra.Data.ContextoConfig;
using W_CC.Infra.Data.Repositorio;

namespace W_CC.Infra.Data.Repositorios.Entidades
{
    public class PessoasRepositorio: RepositorioBase<Pessoas>, IPessoasRepositorio
    {
        public PessoasRepositorio(Contexto context)
            : base(context)
        {

        }

        public bool CPFJaExiste(Pessoas pessoa)
        {
            return Db.Pessoas.Where(p => p.Id != pessoa.Id &&  p.CPF == pessoa.CPF)
                .Select(p => p.Nome).Any();
        }

        public bool NomeJaExiste(Pessoas pessoa)
        {
            return Db.Pessoas.Where(p => p.Nome.ToUpper() == pessoa.Nome.ToUpper() && p.Id != pessoa.Id)
                .Select(p => p.Nome).Any();
        }
    }
}
