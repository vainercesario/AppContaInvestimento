using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using W_CC.Dominio.Model;
using W_CC.Infra.Data.Configuracao.Interfaces;

namespace W_CC.Infra.Data.ContextoConfig
{
    public class Contexto : DbContext
    {

        public Contexto(DbContextOptions options)
            :base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<ContasCorrentes> ContasCorrentes { get; set; }
        public DbSet<Pessoas> Pessoas { get; set; }
        public DbSet<Operacoes> Operacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var registrosEntidades = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(IEntidadeConfig).IsAssignableFrom(x) && !x.IsAbstract).ToList();

            foreach (var tipo in registrosEntidades)
            {
                dynamic configurandoInstancia = Activator.CreateInstance(tipo);
                modelBuilder.ApplyConfiguration(configurandoInstancia);
            }
        }
    }
}
