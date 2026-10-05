
namespace Engineering.Application.Services.TransportationRequests.Models.CreateSnapPaymentOrder;

public class CreateSnapPaymentOrderValidator : AbstractValidator<CreateSnapPaymentOrderRequest>
{
    public CreateSnapPaymentOrderValidator()
    {
        RuleFor(oo => oo.SnapRequestIds).NotNull().NotEmpty().WithError(TransportationRequestErrors.SnapIdsIsEmpty);
    }
}
