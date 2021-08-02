using Microsoft.AspNetCore.Mvc;
using PortalComercio.Models;
using PortalComercio.Repository.Abstract;
using System;

namespace PortalComercio.Controllers
{
    public class VitrineController : Controller
    {
        private readonly IFrutaRepository _frutaRepository;

        public VitrineController(IFrutaRepository frutaRepository)
        {
            _frutaRepository = frutaRepository;
        }

        public IActionResult Index()
        {
            var listaFrutas = _frutaRepository.ObterTodos();

            return View(listaFrutas);
        }

        public ActionResult AtualizarEstoque(Guid id, int quantidade)
        {
            var dtoFruta = _frutaRepository.ObterPorId(id);

            if (dtoFruta == null) return RedirectToAction("Index");

            DtoAtualizarEstoque atualizarEstoque = new DtoAtualizarEstoque()
            {
                Id = id,
                Quantidade = quantidade * (-1)
            };

            _frutaRepository.AtualizarEstoque(atualizarEstoque);

            return RedirectToAction("Index");
        }

        public decimal ObterValorFruta(Guid id) => _frutaRepository.ObterPorId(id).Valor;
    }
}
