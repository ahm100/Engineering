
namespace Engineering.Application.Services.ContractorStatusStatements.Commands.CreateContractorStatusStatementDiscount;

public class CreateContractorStatusStatementDiscountCommandValidator : AbstractValidator<CreateContractorStatusStatementDiscountCommand>
{
    public CreateContractorStatusStatementDiscountCommandValidator()
    {
        RuleFor(oo => oo.ContractorStatusStatement)
            .NotNull().WithError(CSSErrors.InvalidContractorContract);
    }
}
