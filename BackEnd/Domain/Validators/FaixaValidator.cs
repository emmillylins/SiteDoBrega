﻿using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
    public class FaixaValidator : AbstractValidator<Faixa>
    {
        public FaixaValidator()
        {
            RuleFor(x => x.Titulo).MaximumLength(100).WithMessage("O campo '{PropertyName}' deve ter até {MaxLength} caracteres.");

            RuleFor(x => x.Artista).MaximumLength(100).WithMessage("O campo '{PropertyName}' deve ter até {MaxLength} caracteres.");

            RuleFor(x => x.Link).NotNull().NotEmpty().WithMessage("O campo '{PropertyName}' é obrigatório.")
                .MaximumLength(500).WithMessage("O campo '{PropertyName}' deve ter até {MaxLength} caracteres.");

            RuleFor(x => x.NomeUsuario).NotNull().NotEmpty().WithMessage("O campo '{PropertyName}' é obrigatório.");

            RuleFor(x => x.CategoriaId).NotNull().WithMessage("O campo '{PropertyName}' é obrigatório.");
        }
    }
}