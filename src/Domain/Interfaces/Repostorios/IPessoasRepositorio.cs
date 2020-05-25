using W_CC.Dominio.Model;

namespace Dominio.Interfaces.Repostorios
{
    public interface IPessoasRepositorio : IRepositorio<Pessoas>
    {
        bool NomeJaExiste(Pessoas pessoa);
        bool CPFJaExiste(Pessoas pessoa);
    }
}
