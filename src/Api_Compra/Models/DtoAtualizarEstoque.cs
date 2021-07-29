using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Compra.Models
{
    public class DtoAtualizarEstoque
    {
        public Guid Id { get; set; }
        public int Quantidade { get; set; }
    }
}
