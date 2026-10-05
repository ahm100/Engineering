
namespace Engineering.Application.Services.TransportationRequests.Commands.CreateWarehouseTransportation;

public class CreateWarehouseTransportationCommandValidator : AbstractValidator<CreateWarehouseTransportationCommand>
{
    public CreateWarehouseTransportationCommandValidator()
    {
        RuleForEach(oo => oo.Packings)
            .NotNull()
            .NotEmpty()
            .WithError(TransportationRequestErrors.WarehouseCreate);
    }
}
