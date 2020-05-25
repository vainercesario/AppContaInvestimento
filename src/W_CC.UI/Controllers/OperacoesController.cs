using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using W_CC.Aplicacao.DTO;
using W_CC.Aplicacao.Interfaces;

namespace W_CC.UI.Controllers
{
    public class OperacoesController : Controller
    {
        private readonly IOperacoesApp _operacaoApp;

        public OperacoesController(IOperacoesApp operacaoApp)
        {
            _operacaoApp = operacaoApp;
        }

        // GET: OperacoesController
        public ActionResult Index()
        {
            return View(_operacaoApp.Listar());
        }

        // GET: OperacoesController/Depositar
        public ActionResult Depositar()
        {
            ViewBag.Title = "Depositar";
            return View("Create", new OperacoesViewModel()
            {
                TipoOperacao = TipoOperacao.Deposito
            });
        }
        // GET: OperacoesController/Pagar
        public ActionResult Pagar()
        {
            ViewBag.Title = "Pagar";

            return View("Create", new OperacoesViewModel()
            {
                TipoOperacao = TipoOperacao.Pagamento
            });
        }

        // GET: OperacoesController/Resgatar
        public ActionResult Resgatar()
        {
            ViewBag.Title = "Resgatar";
            return View("Create", new OperacoesViewModel()
            {
                TipoOperacao = TipoOperacao.Resgate
            });
        }

        // POST: OperacoesController/Depositar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OperacoesViewModel operacao)
        {
            try
            {
                switch (operacao.TipoOperacao)
                {
                    case TipoOperacao.Deposito:
                        ViewBag.Title = "Depósito";
                        operacao = _operacaoApp.Depositar(operacao);
                        break;
                    case TipoOperacao.Pagamento:
                        ViewBag.Title = "Pagamento";
                        operacao = _operacaoApp.Pagar(operacao);
                        break;
                    case TipoOperacao.Resgate:
                        ViewBag.Title = "Resgate";
                        operacao = _operacaoApp.Resgatar(operacao);
                        break;
                }
                
                if (operacao.Validacoes.Any()) { 
                
                    foreach (var item in operacao.Validacoes)
                        ModelState.AddModelError(item.NomePropriedade, item.Mensagem);

                    return View(operacao);
                }

                return RedirectToAction("Index","ContasCorrentes");
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError("Adicionar Operação", Ex.ToString());
                return View();
            }
        }

        
    }
}
