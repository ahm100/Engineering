
namespace Engineering.Application.Services.TransportationRequests.Queries.GetByIdForPayment;

public class GetTransportationRequestByIdForPaymentQueryValidator : AbstractValidator<GetTransportationRequestByIdForPaymentQuery>
{
    public GetTransportationRequestByIdForPaymentQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TransportationRequestErrors.IdIsEmpty);
    }
}
