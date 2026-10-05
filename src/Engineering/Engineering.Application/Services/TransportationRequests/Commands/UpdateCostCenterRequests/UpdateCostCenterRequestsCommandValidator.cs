
namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateCostCenterRequests;

public class UpdateCostCenterRequestsCommandValidator : AbstractValidator<UpdateCostCenterRequestsCommand>
{
    public UpdateCostCenterRequestsCommandValidator()
    {
        RuleFor(oo => oo.TransportationRequest).NotNull().WithError(TransportationRequestErrors.InValidTypeOfTransport);
        RuleForEach(oo => oo.CostCenters).NotNull().WithError(TransportationRequestErrors.UnValidCostCenters);
    }
}
