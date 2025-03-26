using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
    public class CategoriaValidator : AbstractValidator<Categoria>
    {
        public CategoriaValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Desc).NotNull().NotEmpty().WithMessage("O campo '{PropertyName}' é obrigatório.")
                .MaximumLength(30).WithMessage("O campo '{PropertyName}' deve ter até {MaxLength} caracteres.");
            RuleFor(x => x.Url).NotNull().NotEmpty().WithMessage("O campo '{PropertyName}' é obrigatório.")
                .MaximumLength(30).WithMessage("O campo '{PropertyName}' deve ter até {MaxLength} caracteres.");
            RuleFor(x => x.Img).NotNull().NotEmpty().WithMessage("O campo '{PropertyName}' é obrigatório.")
                .MaximumLength(200).WithMessage("O campo '{PropertyName}' deve ter até {MaxLength} caracteres.");

            RuleForEach(u => u.Faixas).SetValidator(new FaixaValidator());
        }
    }
}
