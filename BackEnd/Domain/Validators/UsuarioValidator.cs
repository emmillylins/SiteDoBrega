using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(x => x.Cpf).NotNull().WithMessage("O campo 'Id' é obrigatório.")
                .MinimumLength(11).MaximumLength(14);
            RuleFor(x => x.Nome).NotNull().NotEmpty().WithMessage("O campo 'Descrição' é obrigatório.");
        }
    }
}
