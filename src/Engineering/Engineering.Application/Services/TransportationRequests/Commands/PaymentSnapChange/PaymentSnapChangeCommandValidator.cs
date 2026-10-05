
namespace Engineering.Application.Services.TransportationRequests.Commands.PaymentSnapChange;

public class PaymentSnapChangeCommandValidator : AbstractValidator<PaymentSnapChangeCommand>
{
    public PaymentSnapChangeCommandValidator()
    {
        RuleFor(c => c.RefrenceId)
            .NotNull().NotEmpty().WithError(CSSErrors.InvalidrefId)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
