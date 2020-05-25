using W_CC.Dominio.Model;

namespace W_CC.Dominio.Interfaces.Servicos
{
    public interface IPessoaServico
    {
        Pessoas Adicionar(Pessoas pessoa);
        Pessoas Atualizar(Pessoas pessoa);
        void Remover(Pessoas pessoa);

    }
}
