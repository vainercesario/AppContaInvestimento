using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using W_CC.Aplicacao.Interfaces;
using W_CC.UI.Models;

namespace W_CC.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContasCorrentesApp _contaApp;
        public HomeController(IContasCorrentesApp contaApp)
        {
            _contaApp = contaApp;
        }


        public IActionResult Index()
        {
            if (_contaApp.ObterConta() == null)
            {
                return RedirectToAction("Create","ContasCorrentes");
            }
            else
            {
                return RedirectToAction("Index", "ContasCorrentes");
            }
        }

    }
}
