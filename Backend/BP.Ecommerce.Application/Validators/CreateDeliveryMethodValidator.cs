using BP.Ecommerce.Application.DTOs;
using FluentValidation;

namespace BP.Ecommerce.Application.Validators
{
    public class CreateDeliveryMethodValidator : AbstractValidator<CreateDeliveryMethodDto>
    {
        public CreateDeliveryMethodValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("No puede ser nulo o vacio");

            RuleFor(x => x.Name)
                .Matches("^[a-zA-Z0-9 ]+$")
                .WithMessage("Solo soporta numeros y letras");  

        }

    }
}
