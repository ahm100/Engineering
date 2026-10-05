
namespace Engineering.Application.Services.ContractorStatusStatements.Models.ContractorStatusStatementDiscountOperations;

public class ContractorStatusStatementDiscountOperationsValidator : AbstractValidator<ContractorStatusStatementDiscountOperationsRequest>
{
    public ContractorStatusStatementDiscountOperationsValidator()
    {
        RuleFor(c => c.ContractorStatusStatementDiscounts)
            .NotNull().WithError(CSSErrors.InValidContractor);

        RuleFor(c => c.ContractorStatusStatementId)
            .NotNull().WithError(CSSErrors.ContractorStatusStatementWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
