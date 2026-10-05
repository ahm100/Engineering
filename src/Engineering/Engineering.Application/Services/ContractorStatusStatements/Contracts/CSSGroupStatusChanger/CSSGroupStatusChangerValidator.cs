
namespace Engineering.Application.Services.ContractorStatusStatements.Models.CSSGroupStatusChanger;

public class CSSGroupStatusChangerValidator : AbstractValidator<CSSGroupStatusChangerRequest>
{
    public CSSGroupStatusChangerValidator()
    {
        RuleFor(c => c.Status)
            .NotNull().WithError(GlobalErrors.StatusIsNull)
            .IsInEnum().WithError(GlobalErrors.StatusNotInEnum);
    }
}
