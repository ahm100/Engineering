
namespace Engineering.Application.Services.ContractorStatusStatements.Models.ContractorStatusStatementStatusChanger;

public class ContractorStatusStatementStatusChangerValidator : AbstractValidator<ContractorStatusStatementStatusChangerRequest>
{
    public ContractorStatusStatementStatusChangerValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithError(CSSErrors.InvalidContractorStatusStatement)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);

        RuleFor(c => c.Status)
            .NotNull().WithError(GlobalErrors.StatusIsNull)
            .IsInEnum().WithError(GlobalErrors.StatusNotInEnum);
    }
}
