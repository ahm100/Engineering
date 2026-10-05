
namespace Engineering.Application.Services.ContractorStatusStatements.Commands.PaymentStatusStatementStatusChange;

public class PaymentStatusStatementStatusChangeCommandValidator : AbstractValidator<PaymentStatusStatementStatusChangeCommand>
{
    public PaymentStatusStatementStatusChangeCommandValidator()
    {
        RuleFor(c => c.RefrenceId)
            .NotNull().NotEmpty().WithError(CSSErrors.InvalidrefId)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);

        RuleFor(c => c.Status)
            .NotNull().WithError(GlobalErrors.StatusIsNull)
            .IsInEnum().WithError(GlobalErrors.StatusNotInEnum);
    }
}
