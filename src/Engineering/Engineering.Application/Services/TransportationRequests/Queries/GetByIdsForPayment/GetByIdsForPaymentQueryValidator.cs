
namespace Engineering.Application.Services.TransportationRequests.Queries.GetByIdsForPayment;

public class GetByIdsForPaymentQueryValidator : AbstractValidator<GetByIdsForPaymentQuery>
{
    public GetByIdsForPaymentQueryValidator()
    {
        RuleFor(oo => oo.Ids).NotNull().NotEmpty().WithError(TransportationRequestErrors.SnapIdsIsEmpty);
    }
}
