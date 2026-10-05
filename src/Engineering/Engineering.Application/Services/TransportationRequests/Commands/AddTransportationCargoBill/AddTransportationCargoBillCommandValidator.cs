
namespace Engineering.Application.Services.TransportationCargos.Commands.AddTransportationCargoBill;

public class AddTransportationCargoBillCommandValidator : AbstractValidator<AddTransportationCargoBillCommand>
{
    public AddTransportationCargoBillCommandValidator()
    {
        RuleFor(oo => oo.TransportationCargo)
            .NotNull()
            .WithError(TransportationRequestErrors.CargosNotfound);
    }
}
