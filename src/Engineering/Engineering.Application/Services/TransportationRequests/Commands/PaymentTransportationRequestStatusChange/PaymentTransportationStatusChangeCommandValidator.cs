namespace Engineering.Application.Services.TransportationRequests.Commands.PaymentTransportationStatusChange;

public class PaymentTransportationStatusChangeCommandValidator : AbstractValidator<PaymentTransportationStatusChangeCommand>
{
    public PaymentTransportationStatusChangeCommandValidator()
    {
        RuleFor(c => c.RefrenceId)
            .NotNull().NotEmpty().WithError(CSSErrors.InvalidrefId)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
