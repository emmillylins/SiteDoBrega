using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
    public class MultaValidator : AbstractValidator<Multa>
    {
        public MultaValidator() 
        {
            RuleFor(x => x.NumeroAIT)
                .NotNull().NotEmpty().WithMessage("O campo '{PropertyName}' é obrigatório.")
                .MaximumLength(50).WithMessage("O campo '{PropertyName}' deve ter até {MaxLength} caracteres.");

            RuleFor(m => m.DataInfracao)
                .NotEmpty().WithMessage("O campo '{PropertyName}' é obrigatório.");

            RuleFor(m => m.CodigoInfracao)
                .NotNull().NotEmpty().WithMessage("O campo '{PropertyName}' é obrigatório.")
                .MaximumLength(20).WithMessage("O campo '{PropertyName}' deve ter até {MaxLength} caracteres.");

            RuleFor(m => m.DescricaoInfracao)
                .NotNull().NotEmpty().WithMessage("O campo '{PropertyName}' é obrigatório.")
                .MaximumLength(200).WithMessage("O campo '{PropertyName}' deve ter até {MaxLength} caracteres.");

            RuleFor(m => m.PlacaVeiculo)
                .NotNull().NotEmpty().WithMessage("O campo '{PropertyName}' é obrigatório.")
                .MaximumLength(10).WithMessage("O campo '{PropertyName}' deve ter até {MaxLength} caracteres.");
        }
    }
}
