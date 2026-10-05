namespace Engineering.Application.Services.ContractorContracts.Commands.Details.UpdateContractorContractDetailCostOver;

public class UpdateContractorContractDetailCostOverCommandValidator : AbstractValidator<UpdateContractorContractDetailCostOverCommand>
{
    public UpdateContractorContractDetailCostOverCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithError(ContractorContractErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(ContractorContractErrors.IdIsEmpty);
    }
}
