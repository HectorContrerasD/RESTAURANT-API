using FluentValidation;
using Restaurant.Api.Models.Entities;
using Restaurant.Api.Payloads;

namespace Restaurant.Api.Repositories.Validators
{
    public class TicketItemValidator : AbstractValidator<TicketItemPayload>
    {
        public TicketItemValidator()
        {
            RuleFor(x => x.ProductoId)
           .NotNull().WithMessage("ProductoId is required.");

            RuleFor(x => x.Cantidad)
                .NotNull().WithMessage("Cantidad is required.")
                .GreaterThan(0).WithMessage("Cantidad must be greater than 0.");
            RuleFor(x=>x.Precio)
        }
    }
}
