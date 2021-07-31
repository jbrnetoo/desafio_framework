using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalComercio.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalComercio.Controllers
{
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
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FrutaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FrutaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FrutaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FrutaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FrutaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FrutaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
