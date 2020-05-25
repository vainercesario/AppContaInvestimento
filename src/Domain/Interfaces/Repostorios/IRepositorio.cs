using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Interfaces.Repostorios
{
    public interface IRepositorio<T> : IDisposable where T : class
    {
        T Adicionar(T obj);
        T Atualizar(T obj);
        void Remover(T obj);
        T ObterPorId(Guid id);
        IEnumerable<T> Listar();
    }
}
