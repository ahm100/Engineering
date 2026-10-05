namespace Engineering.Application.Services.ContractorContracts.Commands.Details.CreateContractorContractDetailCostOver;

public class CreateContractorContractDetailCostOverCommandValidator : AbstractValidator<CreateContractorContractDetailCostOverCommand>
{
    public CreateContractorContractDetailCostOverCommandValidator()
    {
        RuleFor(c => c.CostOver)
            .NotNull().WithError(ContractorContractErrors.CostOverIsEmpty);
    }
}
