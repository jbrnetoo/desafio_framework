using System;

namespace Domain.Entidades
{
    public class Fruta : Entity
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public decimal Valor { get; set; }
        public int Quantidade { get; set; }
    }
}
