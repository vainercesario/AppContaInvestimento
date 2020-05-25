using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dominio.Interfaces.Repostorios;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using W_CC.Aplicacao.Aplicacoes;
using W_CC.Aplicacao.AutoMapper;
using W_CC.Aplicacao.Interfaces;
using W_CC.Dominio.Interfaces.Repostorios;
using W_CC.Dominio.Interfaces.Servicos;
using W_CC.Dominio.Servicos;
using W_CC.Infra.Data.ContextoConfig;
using W_CC.Infra.Data.Interfaces;
using W_CC.Infra.Data.Repositorio;
using W_CC.Infra.Data.Repositorios.Entidades;
using W_CC.Infra.Data.UoW;

namespace W_CC.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration["ConnectionStrings:contaCorrente"];
            services.AddDbContext<Contexto>(options =>
                options.UseMySQL(connection)
            );

            services.AddControllersWithViews();

            RegistrarLifeStyle(services);

            RegistrarAutoMapper(services);

            RegistrarAcessoSeguro(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }

        private void RegistrarLifeStyle(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepositorio<>), typeof(RepositorioBase<>));
            services.AddScoped<ITransacao, Transacao>();
            services.AddScoped<IContasCorrentesServico, ContasCorrentesServico>();
            services.AddScoped<IContasCorrentesRepositorio, ContasCorrentesRepositorio>();
            services.AddScoped<IContasCorrentesApp, ContasCorrentesApp>();
            
            services.AddScoped<IPessoasApp, PessoasApp>();
            services.AddScoped<IPessoasRepositorio, PessoasRepositorio>();
            services.AddScoped<IPessoaServico, PessoasServico>();

            services.AddScoped<IOperacoesApp, OperacoesApp>();
            services.AddScoped<IOperacoesRepositorio, OperacoesRepositorio>();
            services.AddScoped<IOperacoesServico, OperacoesServico>();
        }

        private void RegistrarAutoMapper(IServiceCollection services)
        {
            AutoMapping autoMapping = new AutoMapping();
            var config = autoMapping.Configuracao();
            IMapper mapper = config.CreateMapper();

            services.AddSingleton(mapper);
        }

        private void RegistrarAcessoSeguro(IServiceCollection services)
        {
            services.AddHttpsRedirection(options =>
            {
                options.HttpsPort = 443;
            });
        }
    }
}
