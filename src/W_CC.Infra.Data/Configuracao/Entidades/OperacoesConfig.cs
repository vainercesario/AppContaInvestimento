using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using W_CC.Dominio.Model;
using W_CC.Infra.Data.Configuracao.Interfaces;

namespace W_CC.Infra.Data.Configuracao.Entidades
{
    public class OperacoesConfig : ConfigBase<Operacoes>, IEntityTypeConfiguration<Operacoes>, IEntidadeConfig
    {
        public void Configure(EntityTypeBuilder<Operacoes> entidade)
        {
            Padronizacao(entidade, nomeTabela: "Operacoes");

            
        }
    }
}
