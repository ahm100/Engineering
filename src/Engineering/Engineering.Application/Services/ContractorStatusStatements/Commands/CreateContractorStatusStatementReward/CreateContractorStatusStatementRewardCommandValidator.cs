
namespace Engineering.Application.Services.ContractorStatusStatements.Commands.CreateContractorStatusStatementReward;

public class CreateContractorStatusStatementRewardCommandValidator : AbstractValidator<CreateContractorStatusStatementRewardCommand>
{
    public CreateContractorStatusStatementRewardCommandValidator()
    {
        RuleFor(oo => oo.Entity)
            .NotNull().WithError(CSSErrors.InvalidContractorContract);
    }
}
