
namespace Engineering.Application.Services.TransportationRequests.Models.GroupTransportationRequestStatusChanger;

public class GroupTransportationRequestStatusChangerValidator : AbstractValidator<GroupTransportationRequestStatusChangerRequest>
{
    public GroupTransportationRequestStatusChangerValidator()
    {
        RuleFor(c => c.Ids)
            .NotEmpty().WithError(GlobalErrors.IdsIsEmpty)
            .NotNull().WithError(GlobalErrors.IdsIsNull);

        RuleForEach(c => c.Ids)
            .GreaterThan(0).WithError(GlobalErrors.IdsLessThanOrEqualZero);

        RuleFor(oo => oo.Status)
            .NotNull().WithError(GlobalErrors.StatusIsNull)
            .IsInEnum().WithError(GlobalErrors.StatusNotInEnum);
    }
}
