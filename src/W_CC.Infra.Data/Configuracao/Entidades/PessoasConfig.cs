using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using W_CC.Dominio.Model;
using W_CC.Infra.Data.Configuracao.Interfaces;

namespace W_CC.Infra.Data.Configuracao.Entidades
{
    public class PessoasConfig : ConfigBase<Pessoas>, IEntityTypeConfiguration<Pessoas>, IEntidadeConfig
    {
        public void Configure(EntityTypeBuilder<Pessoas> entidade)
        {
            Padronizacao(entidade, nomeTabela: "Pessoas");

            entidade
                .HasIndex(p => new { p.CPF }).IsUnique();

            entidade
                .Property(p => p.Nome)
                .HasMaxLength(120)
                .IsRequired();

            entidade
                .HasMany(p => p.ContasCorrentes)   
                .WithOne(cc => cc.Pessoa)       
                .HasForeignKey(cc => cc.PessoaId);

        }
    }
}
