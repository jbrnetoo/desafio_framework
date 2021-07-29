using System;
using Microsoft.AspNetCore.Mvc;

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
                var teste =  new { Teste = "Teste" };

                return Ok(teste);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
