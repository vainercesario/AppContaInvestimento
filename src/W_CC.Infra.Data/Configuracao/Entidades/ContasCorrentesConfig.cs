using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using W_CC.Dominio.Model;
using W_CC.Infra.Data.Configuracao.Interfaces;

namespace W_CC.Infra.Data.Configuracao.Entidades
{
    public class ContasCorrentesConfig : ConfigBase<ContasCorrentes>, IEntityTypeConfiguration<ContasCorrentes>, IEntidadeConfig
    {
        public void Configure(EntityTypeBuilder<ContasCorrentes> entidade)
        {
            Padronizacao(entidade, nomeTabela: "ContasCorrentes");

            entidade
                .HasIndex(cc => new { cc.Agencia, cc.Conta }).IsUnique();

            entidade
                .Property(cc => cc.Agencia)
                .HasMaxLength(4)
                .IsRequired();

            entidade
                .Property(cc => cc.Conta)
                .HasMaxLength(10)
                .IsRequired();

            entidade
                .HasMany(cc => cc.Operacoes)
                .WithOne(o => o.ContaCorrente)
                .HasForeignKey(cc => cc.ContaCorrenteId);

        }
    }
}
