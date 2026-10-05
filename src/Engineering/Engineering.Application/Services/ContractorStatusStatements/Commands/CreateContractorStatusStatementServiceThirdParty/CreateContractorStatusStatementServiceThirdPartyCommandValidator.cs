
namespace Engineering.Application.Services.ContractorStatusStatements.Commands.CreateContractorStatusStatementServiceThirdParty;

public class CreateContractorStatusStatementServiceThirdPartyCommandValidator : AbstractValidator<CreateContractorStatusStatementServiceThirdPartyCommand>
{
    public CreateContractorStatusStatementServiceThirdPartyCommandValidator()
    {
        RuleFor(oo => oo.Entity)
            .NotNull().WithError(CSSErrors.InvalidContractorContract);
    }
}
