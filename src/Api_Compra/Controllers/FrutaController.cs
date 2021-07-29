using Domain.Entidades;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace Api_Compra.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FrutaController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult ObterFrutas()
        {
            try
            {
                var teste = new { Teste = "Teste" };

                return Ok(teste);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        public ActionResult Adicionar(Fruta fruta)
        {
            if (!ModelState.IsValid) return BadRequest();

            var imagemNome = Guid.NewGuid() + "_" + fruta.Nome;
            if (UploadImagem(fruta.Foto, imagemNome))
                return BadRequest();

            // Se não adicione no banco

            return Ok();
        }

        private bool UploadImagem(string arquivo, string imgNome)
        {
            var imagemDataByteArray = Convert.FromBase64String(arquivo);

            if (string.IsNullOrEmpty(arquivo))
            {
                //Do Something
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwrooot/imagens", imgNome);

            if (System.IO.File.Exists(filePath))
            {
                // Se já existir faça algo
            }

            System.IO.File.WriteAllBytes(filePath, imagemDataByteArray);
            return true;
        }
    }
}
