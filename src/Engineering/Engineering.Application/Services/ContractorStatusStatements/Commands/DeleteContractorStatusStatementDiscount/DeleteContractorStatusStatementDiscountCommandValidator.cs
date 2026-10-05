
namespace Engineering.Application.Services.ContractorStatusStatements.Commands.DeleteContractorStatusStatementDiscount;

public class DeleteContractorStatusStatementDiscountCommandValidator : AbstractValidator<DeleteContractorStatusStatementDiscountCommand>
{
    public DeleteContractorStatusStatementDiscountCommandValidator()
    {
        RuleFor(oo => oo.Id)
            .NotNull().WithError(CSSErrors.DiscountIdIsNull);
    }
}
