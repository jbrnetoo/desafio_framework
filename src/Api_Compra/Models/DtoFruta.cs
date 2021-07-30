using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;

namespace Api_Compra.Models
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

    public class DtoFrutaValidator : AbstractValidator<DtoFruta>
    {
        public DtoFrutaValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome não pode ser vazio.")
                .Length(1, 100).WithMessage("Tamanho ({TotalLength}) do {PropertyName} inválido");

            RuleFor(x => x.Descricao)
               .NotEmpty().WithMessage("Descrição não pode ser vazia.")
               .Length(1, 300).WithMessage("Tamanho ({TotalLength}) do {PropertyName} inválido");

            RuleFor(x => x.Imagem)
               .NotEmpty().WithMessage("Imagem não pode ser vazia.")
               .Length(1, 500).WithMessage("Tamanho ({TotalLength}) do {PropertyName} inválido");

            RuleFor(x => x.Valor)
                 .NotEmpty().WithMessage("O valor da fruta não pode ser nulo");

            RuleFor(x => x.Quantidade)
                .NotNull().WithMessage("A quantidade não pode ser vazia")
                .Must(x => x >= 0).WithMessage("Quantidade inválida: ({PropertyValue})");
        }
    }
}
