namespace Engineering.Application.Services.ContractorContracts.Commands.Details.CreateContractorContractDetailService;

public class CreateContractorContractDetailServiceCommandValidator : AbstractValidator<CreateContractorContractDetailServiceCommand>
{
    public CreateContractorContractDetailServiceCommandValidator()
    {
        RuleFor(c => c.ContractorContractDetail)
            .NotNull().WithError(ContractorContractErrors.DetailIsEmpty);

        RuleFor(oo => oo.ContractorContractDetailService)
            .NotNull().WithError(ContractorContractErrors.ServiceInfoIsEmpty);
    }
}
