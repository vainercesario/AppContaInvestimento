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
    public class ContasCorrentesController : Controller
    {
        private readonly IContasCorrentesApp _contaCorrente;

        public ContasCorrentesController(IContasCorrentesApp contaCorrente)
        {
            _contaCorrente = contaCorrente;
        }
        // GET: ContasCorrentes
        public ActionResult Index()
        {
            var conta = _contaCorrente.ObterConta();

            if (conta == null)
                return RedirectToAction("Index","Home");

            return View(conta);
        }

        // GET: ContasCorrentes/Details/5
        public ActionResult Details(Guid id)
        {
            return View(_contaCorrente.ObterPorId(id));
        }

        // GET: ContasCorrentes/Create
        public ActionResult Create()
        {
            return View(new ContasCorrentesViewModel());
        }

        // POST: ContasCorrentes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContasCorrentesViewModel contaCorrente)
        {
            try
            {
                _contaCorrente.Adicionar(contaCorrente);
                if (contaCorrente.Validacoes.Any())
                {
                    foreach (var item in contaCorrente.Validacoes)
                        ModelState.AddModelError(item.NomePropriedade, item.Mensagem);

                    return View(contaCorrente);
                }

                return RedirectToAction("Index", "ContasCorrentes");
            }
            catch
            {
                return View(contaCorrente);
            }
        }

        // GET: ContasCorrentes/Edit/5
        public ActionResult Edit(Guid id)
        {
            return View(_contaCorrente.ObterPorId(id));
        }

        // POST: ContasCorrentes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ContasCorrentesViewModel contaCorrente)
        {
            try
            {
                // TODO: Add update logic here
                _contaCorrente.Atualizar(contaCorrente);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ContasCorrentes/Delete/5
        public ActionResult Delete(Guid id)
        {
            return View(_contaCorrente.ObterPorId(id));
        }

        // POST: ContasCorrentes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ContasCorrentesViewModel contaCorrente)
        {
            try
            {
                // TODO: Add delete logic here
                _contaCorrente.Remover(contaCorrente);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}