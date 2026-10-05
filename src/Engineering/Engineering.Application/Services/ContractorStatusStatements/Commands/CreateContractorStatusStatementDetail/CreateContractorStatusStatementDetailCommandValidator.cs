
namespace Engineering.Application.Services.ContractorStatusStatements.Commands.CreateContractorStatusStatementDetail;

public class CreateContractorStatusStatementDetailCommandValidator : AbstractValidator<CreateContractorStatusStatementDetailCommand>
{
    public CreateContractorStatusStatementDetailCommandValidator()
    {
        RuleFor(oo => oo.Entity)
            .NotNull().WithError(CSSErrors.InvalidContractorContract);
    }
}
