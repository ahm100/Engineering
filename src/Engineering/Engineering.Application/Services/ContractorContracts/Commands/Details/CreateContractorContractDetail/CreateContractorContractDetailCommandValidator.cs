namespace Engineering.Application.Services.ContractorContracts.Commands.Details.CreateContractorContractDetail;

public class CreateContractorContractDetailCommandValidator : AbstractValidator<CreateContractorContractDetailCommand>
{
    public CreateContractorContractDetailCommandValidator()
    {
        RuleFor(c => c.ContractorContract)
            .NotNull().WithError(ContractorContractErrors.ContractorContractIdIsEmpty);
    }
}
