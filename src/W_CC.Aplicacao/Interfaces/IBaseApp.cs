using System;
using System.Collections.Generic;
using System.Text;

namespace W_CC.Aplicacao.Interfaces
{
    public interface IBaseApp<T> where T : class
    {
        T Adicionar(T obj);
        T Atualizar(T obj);
        void Remover(T obj);
        T ObterPorId(Guid id);
        IEnumerable<T> Listar();
    }
}
