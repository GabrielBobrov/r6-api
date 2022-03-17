using FluentValidation;
using R6.Domain.Entities;

namespace R6.Domain.Validators{
    public class OperatorValidator : AbstractValidator<Operator>{
        public OperatorValidator()
        {
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage("A entidade não pode ser vazia.")

                .NotNull()
                .WithMessage("A entidade não pode ser nula.");

            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("O nome não pode ser nulo.")

                .NotEmpty()
                .WithMessage("O nome não pode ser vazio.")
                
                .MinimumLength(3)
                .WithMessage("O nome deve ter no mínimo 3 caracteres.")

                .MaximumLength(80)
                .WithMessage("O nome deve ter no máximo 80 caracteres.");

            RuleFor(x => x.Dificult)
                .NotNull()
                .WithMessage("É necessário informar a dificuldade do operador.");

            RuleFor(x => x.Speed)
                .NotNull()
                .WithMessage("É necessário informar a dificuldade do operador.");

            RuleFor(x => x.Armor)
                .NotNull()
                .WithMessage("É necessário informar a armadura do operador.");
        }
    }
}