namespace Engineering.Application.Services.ContractorContracts.Commands.Details.DeleteContractorContractDetail;

public class DeleteContractorContractDetailCommandValidator : AbstractValidator<DeleteContractorContractDetailCommand>
{
    public DeleteContractorContractDetailCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithError(ContractorContractDetailErrors.InvalidContractorContractDetailId);
    }
}