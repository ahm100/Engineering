
namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateWarehouseRequests;

public class UpdateWarehouseRequestsCommandValidator : AbstractValidator<UpdateWarehouseRequestsCommand>
{
    public UpdateWarehouseRequestsCommandValidator()
    {
        RuleFor(oo => oo.TransportationRequest).NotNull().WithError(TransportationRequestErrors.InValidTypeOfTransport);
        RuleForEach(oo => oo.Warehouses).NotNull().WithError(TransportationRequestErrors.UnvalidPackingWarehouse);
    }
}
