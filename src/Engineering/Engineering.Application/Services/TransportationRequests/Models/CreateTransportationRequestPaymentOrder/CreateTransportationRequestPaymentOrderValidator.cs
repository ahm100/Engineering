
namespace Engineering.Application.Services.TransportationRequests.Models.CreateTransportationRequestPaymentOrder;

public class CreateTransportationRequestPaymentOrderValidator : AbstractValidator<CreateTransportationRequestPaymentOrderRequest>
{
    public CreateTransportationRequestPaymentOrderValidator()
    {
        RuleFor(oo => oo.TransportationRequestId).NotNull().NotEmpty().WithError(TransportationRequestErrors.IdIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
