using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using W_CC.Dominio.Model;

namespace W_CC.Infra.Data.Configuracao
{
    public class ConfigBase<TEntity> where TEntity : EntidadeBase
    {
        public void Padronizacao(EntityTypeBuilder<TEntity> entidade, string nomeTabela)
        {
            entidade.ToTable(nomeTabela);
            entidade.HasKey(x => x.Id);
        }
    }
}
