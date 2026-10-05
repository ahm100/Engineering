
namespace Engineering.Application.Services.TransportationRequests.Models.ChangeToPaid;

public class ChangeToPaidTransportationRequestValidator : AbstractValidator<ChangeToPaidTransportationRequestRequest>
{
    public ChangeToPaidTransportationRequestValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TransportationRequestErrors.IdIsEmpty);
        RuleFor(oo => oo.ManagerDescription).NotEmpty().WithError(TransportationRequestErrors.ManagerDescriptionIsEmpty);
    }
}
