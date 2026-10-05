
namespace Engineering.Application.Services.RequestMachineryStatusStatements.Commands.RequestMachineryStatusStatementStatusChanger;

public class RequestMachineryStatusStatementStatusChangerCommandValidator : AbstractValidator<RequestMachineryStatusStatementStatusChangerCommand>
{
    public RequestMachineryStatusStatementStatusChangerCommandValidator()
    {
        RuleFor(c => c.Entity)
            .NotNull().WithError(RequestMachineryStatusStatementErrors.RequestMachineryStatusStatementWithIdNotFound);

        RuleFor(c => c.Status)
            .NotNull().WithError(GlobalErrors.StatusIsNull)
            .IsInEnum().WithError(GlobalErrors.StatusNotInEnum);
    }
}
