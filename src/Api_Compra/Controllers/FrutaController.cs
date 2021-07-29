using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Compra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FrutaController : ControllerBase
    {
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
