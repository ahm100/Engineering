namespace Engineering.Application.Services.TransportationRequests.Models.CreateAirPlanePaymentOrder;

public class CreateAirPlanePaymentOrderValidator : AbstractValidator<CreateAirPlanePaymentOrderRequest>
{
    public CreateAirPlanePaymentOrderValidator()
    {
        RuleFor(oo => oo.AirPlaneRequestId).NotNull().NotEmpty().WithError(TransportationRequestErrors.IdIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
