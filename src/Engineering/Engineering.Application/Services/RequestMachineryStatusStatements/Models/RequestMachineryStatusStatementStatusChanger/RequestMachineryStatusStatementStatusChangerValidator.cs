
namespace Engineering.Application.Services.RequestMachineryStatusStatements.Models.RequestMachineryStatusStatementStatusChanger;

public class RequestMachineryStatusStatementStatusChangerValidator : AbstractValidator<RequestMachineryStatusStatementStatusChangerRequest>
{
    public RequestMachineryStatusStatementStatusChangerValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithError(RequestMachineryStatusStatementErrors.InValidId)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);

        RuleFor(c => c.Status)
            .NotNull().WithError(GlobalErrors.StatusIsNull)
            .IsInEnum().WithError(GlobalErrors.StatusNotInEnum);
    }
}
