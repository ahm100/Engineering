namespace Engineering.Application.Services.ContractorContracts.Commands.ContractorContractHeaders.ContractorContractHeaderStatusChanger;

public class ContractorContractHeaderStatusChangerCommandValidator : AbstractValidator<ContractorContractHeaderStatusChangerCommand>
{
    public ContractorContractHeaderStatusChangerCommandValidator()
    {
        RuleFor(c => c.Entity)
            .NotNull().WithError(ContractorContractErrors.InValidContractorContractId);

        RuleFor(c => c.Status)
            .NotNull().WithError(GlobalErrors.StatusIsNull)
            .IsInEnum().WithError(GlobalErrors.StatusNotInEnum);
    }
}
