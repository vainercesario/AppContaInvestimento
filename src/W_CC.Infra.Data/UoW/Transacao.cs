using System;
using System.Collections.Generic;
using System.Text;
using W_CC.Infra.Data.ContextoConfig;
using W_CC.Infra.Data.Interfaces;

namespace W_CC.Infra.Data.UoW
{
    public class Transacao : ITransacao
    {
        private readonly Contexto _contexto;
        public Transacao(Contexto contexto)
        {
            _contexto = contexto;
        }

        public void Commit()
        {
            _contexto.SaveChanges();
        }
    }
}
