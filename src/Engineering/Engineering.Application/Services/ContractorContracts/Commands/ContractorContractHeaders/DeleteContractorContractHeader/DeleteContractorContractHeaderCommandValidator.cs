namespace Engineering.Application.Services.ContractorContracts.Commands.ContractorContractHeaders.DeleteContractorContractHeader;

public class DeleteContractorContractHeaderCommandValidator : AbstractValidator<DeleteContractorContractHeaderCommand>
{
    public DeleteContractorContractHeaderCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithError(ContractorContractErrors.InValidContractorContractId)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
