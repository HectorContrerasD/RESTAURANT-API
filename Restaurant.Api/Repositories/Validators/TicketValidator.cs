using FluentValidation;
using Restaurant.Api.Payloads;

namespace Restaurant.Api.Repositories.Validators
{
    public class TicketValidator : AbstractValidator<TicketPayload>
    {
        public TicketValidator()
        {
            RuleFor(x => x.MesaId)
                .NotNull().WithMessage("El id de la mesa es requerido.")
                .GreaterThan(0).WithMessage("El id de la mesa debe ser mayor que 0.");
            RuleFor(x => x.TicketItems)
                .NotEmpty().WithMessage("Se necesita un producto por lo menos.")
                .Must(items => items.All(item => item.Cantidad > 0))
                .WithMessage("La cantidad de productos tiene que se mayor que 0.");
            RuleForEach(x => x.TicketItems)
                .SetValidator(new TicketItemValidator());
        }
    }
}
