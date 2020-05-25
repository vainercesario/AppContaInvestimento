using Dominio.Interfaces.Repostorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using W_CC.Dominio.Model;
using W_CC.Infra.Data.ContextoConfig;

namespace W_CC.Infra.Data.Repositorio
{
    public class RepositorioBase<T> : IRepositorio<T>, IDisposable where T : EntidadeBase, new()
    {
        protected Contexto Db;
        protected DbSet<T> DbSet;

        public RepositorioBase(Contexto contexto)
        {
            Db = contexto;
            DbSet = Db.Set<T>();
        }

        public virtual T Adicionar(T obj)
        {
            var newObj = DbSet.Add(obj);
            return newObj.Entity;
        }

        public virtual T Atualizar(T obj)
        {
            var newObj = DbSet.Update(obj);
            return newObj.Entity;
        }

        public virtual void Remover(T obj)
        {
            DbSet.Remove(obj);
            Db.SaveChanges();
        }

        public IEnumerable<T> Listar()
        {
            return DbSet.AsNoTracking().ToList();
        }

        public T ObterPorId(Guid id)
        {
            return DbSet.Find(id);
        }

        
        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }

    }
}
