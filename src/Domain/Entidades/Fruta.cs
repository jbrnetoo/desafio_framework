using System;

namespace Domain.Entidades
{
    public class Fruta
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Foto { get; set; }
        public decimal Valor { get; set; }
        public int Quantidade { get; set; }
    }
}
