
namespace Engineering.Application.Services.RequestMachineryStatusStatements.Commands.PaymentRequestMachineryStatusChange;

public class PaymentRequestMachineryStatusChangeCommandValidator : AbstractValidator<PaymentRequestMachineryStatusChangeCommand>
{
    public PaymentRequestMachineryStatusChangeCommandValidator()
    {
        RuleFor(c => c.RefrenceId)
            .NotNull().NotEmpty().WithError(CSSErrors.InvalidrefId)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
