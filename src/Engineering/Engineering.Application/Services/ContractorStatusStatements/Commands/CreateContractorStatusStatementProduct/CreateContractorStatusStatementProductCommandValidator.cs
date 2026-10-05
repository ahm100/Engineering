
namespace Engineering.Application.Services.ContractorStatusStatements.Commands.CreateContractorStatusStatementProduct;

public class CreateContractorStatusStatementProductCommandValidator : AbstractValidator<CreateContractorStatusStatementProductCommand>
{
    public CreateContractorStatusStatementProductCommandValidator()
    {
        RuleFor(oo => oo.Entity)
            .NotNull().WithError(CSSErrors.InvalidContractorContract);
    }
}
