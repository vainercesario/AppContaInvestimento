using W_CC.Aplicacao.DTO;

namespace W_CC.Aplicacao.Interfaces
{
    public interface IContasCorrentesApp : IBaseApp<ContasCorrentesViewModel>
    {
        ContasCorrentesViewModel ObterConta();
    }
}
