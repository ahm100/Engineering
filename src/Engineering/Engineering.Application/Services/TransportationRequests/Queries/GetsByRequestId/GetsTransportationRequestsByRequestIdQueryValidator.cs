using Engineering.Application.Services.TransportationRequests.Queries.GetPaidByDate;

namespace Engineering.Application.Services.TransportationRequests.Queries.GetByDate;

public class GetsTransportationRequestsByRequestIdQueryValidator : AbstractValidator<GetsTransportationRequestsByRequestIdQuery>
{
    public GetsTransportationRequestsByRequestIdQueryValidator()
    {
        RuleFor(oo => oo.RequestById).NotNull().WithError(TransportationRequestErrors.RequestByIdIsEmpty);
        RuleFor(oo => oo.StartDate).NotEmpty().WithError(TransportationRequestErrors.StartDateIsEmpty);
        RuleFor(oo => oo.EndDate).NotEmpty().WithError(TransportationRequestErrors.EndDateIsEmpty);
        RuleFor(oo => oo.Status).IsInEnum().WithError(TransportationRequestErrors.StatusIsEmpty);
    }
}
