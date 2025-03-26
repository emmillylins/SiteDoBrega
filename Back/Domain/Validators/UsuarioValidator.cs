using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(x => x.Cpf).NotNull().NotEmpty().WithMessage("O campo '{PropertyName}' é obrigatório.")
                .MinimumLength(11).MaximumLength(14).WithMessage("O campo '{PropertyName}' deve ter entre {MinLength} e {MaxLength} caracteres.");
            
            RuleFor(x => x.Nome).NotNull().NotEmpty().WithMessage("O campo '{PropertyName}' é obrigatório.")
                .MaximumLength(100).WithMessage("O campo '{PropertyName}' deve ter até {MaxLength} caracteres."); 
            
            RuleFor(x => x.NomeUsuario).NotNull().NotEmpty().WithMessage("O campo '{PropertyName}' é obrigatório.")
                .MaximumLength(30).WithMessage("O campo '{PropertyName}' deve ter até {MaxLength} caracteres.");
            
            RuleFor(x => x.DataNasc).Length(8).WithMessage("O campo '{PropertyName}' deve ter até {Length} caracteres.");
            
            RuleForEach(u => u.Faixas).SetValidator(new FaixaValidator());
        }
    }
}
