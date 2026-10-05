
namespace Engineering.Application.Services.TransportationRequests.Commands.AddTransportationRequestBill;

public class AddTransportationRequestBillCommandValidator : AbstractValidator<AddTransportationRequestBillCommand>
{
    public AddTransportationRequestBillCommandValidator()
    {
        RuleFor(oo => oo.TransportationRequest).NotNull().WithError(TransportationRequestErrors.InValidTypeOfTransport);
    }
}
