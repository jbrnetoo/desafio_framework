using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalComercio.Models;
using PortalComercio.Repository.Abstract;
using System;

namespace PortalComercio.Controllers
{
    //[Authorize]
    public class FrutaController : Controller
    {
        private readonly IFrutaRepository _frutaRepository;

        public FrutaController(IFrutaRepository frutaRepository)
        {
            _frutaRepository = frutaRepository;
        }

        // GET: FrutaController
        public ActionResult Index()
        {
            var listaFrutas = _frutaRepository.ObterTodos();

            return View(listaFrutas);
        }

        // GET: FrutaController/Details/5
        public ActionResult Details(Guid id)
        {
            var fruta = _frutaRepository.ObterPorId(id);

            if (fruta == null)
                return NotFound();

            return View(fruta);
        }

        // GET: FrutaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FrutaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DtoFruta dtoFruta)
        {
            if (ModelState.IsValid)
            {
                dtoFruta.Id = Guid.NewGuid();
                var result = _frutaRepository.InserirFruta(dtoFruta);

                if (result)
                    return RedirectToAction("Index");
                else
                    return View(dtoFruta);
            }

            return View(dtoFruta);
        }

        // GET: FrutaController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var fruta = _frutaRepository.ObterPorId(id);

            if (fruta == null)
                return NotFound();

            return View(fruta);
        }

        // POST: FrutaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, DtoFruta dtoFruta)
        {
            if (id != dtoFruta.Id) return NotFound();

            if (!ModelState.IsValid) return View(dtoFruta);

            var result = _frutaRepository.AtualizarFruta(dtoFruta);

            if (result)
                return RedirectToAction("Index");
            else
                return View(dtoFruta);
        }

        // GET: FrutaController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var fruta = _frutaRepository.ObterPorId(id);

            if (fruta == null)
                return NotFound();

            return View(fruta);
        }

        // POST: FrutaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var fruta = _frutaRepository.ObterPorId(id);

            if (fruta == null)
                return NotFound();

            _frutaRepository.RemoverFruta(id);

            return RedirectToAction("Index");
        }
    }
}
