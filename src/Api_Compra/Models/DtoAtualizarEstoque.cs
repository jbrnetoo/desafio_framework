using System;

namespace Api_Compra.Models
{
    public class DtoAtualizarEstoque
    {
        public Guid Id { get; set; }
        public int Quantidade { get; set; }
    }
}
