﻿using FluentValidation;
using GeekStore.Business.Dto_s;

namespace GeekStore.Business.Dto_s.Validations
{
    public class ProdutoValidation : AbstractValidator<Produto>
    {
        public ProdutoValidation()
        {
            RuleFor(x => x.Descricao)
                .NotEmpty()
                    .WithMessage("O campo {PropertyName} precisa ser preenchido.")
                .Length(3, 100)
                    .WithMessage("O valor do campo {PropertyName} deve conter entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(x => x.Preco)
                .GreaterThanOrEqualTo(0)
                    .WithMessage("O valor do campo {PropertyName} não pode ser negativo.");
        }
    }
}