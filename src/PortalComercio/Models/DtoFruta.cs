using Microsoft.AspNetCore.Http;
using System;

namespace PortalComercio.Models
{
    public class DtoFruta
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public IFormFile ImagemUpload { get; set; }
        public decimal Valor { get; set; }
        public int Quantidade { get; set; }
    }
}
