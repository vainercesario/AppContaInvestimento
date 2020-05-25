using AutoMapper;
using W_CC.Aplicacao.DTO;
using W_CC.Aplicacao.DTO.Validacoes;
using W_CC.Dominio.Model;
using W_CC.Dominio.Validacoes;

namespace W_CC.Aplicacao.AutoMapper
{
    public class AutoMapping : Profile
    {

        public MapperConfiguration Configuracao()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ContasCorrentes, ContasCorrentesViewModel>().ReverseMap();
                cfg.CreateMap<Pessoas, PessoasViewModel>().ReverseMap();
                cfg.CreateMap<Operacoes, OperacoesViewModel>().ReverseMap();
                cfg.CreateMap<Validacao, ValidacaoViewModel>().ReverseMap();
                cfg.CreateMap<ItemValidacao, ItemValidacaoViewModel>().ReverseMap();
            });
        }
    }
}
