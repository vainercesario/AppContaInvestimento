using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Globalization;

namespace W_CC.Infra.CrossCutting
{
    public class RegrasAcessos : ActionFilterAttribute
    {
        private readonly string _action;
        private readonly string _controller;

        public RegrasAcessos(string action, string controller)
        {
            _action = action;
            _controller = controller;
        }
    }
}
