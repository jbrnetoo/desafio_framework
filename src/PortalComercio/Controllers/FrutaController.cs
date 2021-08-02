using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalComercio.Models;
using PortalComercio.Repository.Abstract;
using System;
using System.IO;

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

                var imgPrefixo = Guid.NewGuid() + "_";

                if (!UploadArquivo(dtoFruta.ImagemUpload, imgPrefixo))
                {
                    return View(dtoFruta);
                }

                dtoFruta.Imagem = imgPrefixo + dtoFruta.ImagemUpload.FileName;
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

            var frutaAtualizacao = _frutaRepository.ObterPorId(id);
            dtoFruta.Imagem = frutaAtualizacao.Imagem;

            if (dtoFruta.ImagemUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";
                if (!UploadArquivo(dtoFruta.ImagemUpload, imgPrefixo))
                {
                    return View(dtoFruta);
                }

                frutaAtualizacao.Imagem = imgPrefixo + dtoFruta.ImagemUpload.FileName;
            }

            frutaAtualizacao.Nome = dtoFruta.Nome;
            frutaAtualizacao.Descricao = dtoFruta.Descricao;
            frutaAtualizacao.Valor = dtoFruta.Valor;
            frutaAtualizacao.Quantidade = dtoFruta.Quantidade;

            var result = _frutaRepository.AtualizarFruta(frutaAtualizacao);

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

        private bool UploadArquivo(IFormFile arquivo, string imgPrefixo)
        {
            if (arquivo.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imgPrefixo + arquivo.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                arquivo.CopyToAsync(stream);
            }

            return true;
        }
    }
}
