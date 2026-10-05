
namespace Engineering.Application.Services.ContractorStatusStatements.Commands.UpdateContractorStatusStatementDiscount;

public class UpdateContractorStatusStatementDiscountCommandValidator : AbstractValidator<UpdateContractorStatusStatementDiscountCommand>
{
    public UpdateContractorStatusStatementDiscountCommandValidator()
    {
        RuleFor(oo => oo.Id)
            .NotNull().WithError(CSSErrors.DiscountIdIsNull);
    }
}
