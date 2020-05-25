using System;
using System.Collections.Generic;
using System.Text;
using W_CC.Infra.Data.Interfaces;

namespace W_CC.Aplicacao.Aplicacoes
{
    public abstract class BaseApp
    {
        private readonly ITransacao _trans;

        protected BaseApp(ITransacao trans)
        {
            _trans = trans;
        }

        public void Commit()
        {
            _trans.Commit();
        }
    }
}
