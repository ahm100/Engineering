namespace Engineering.Application.Services.ContractorContracts.Commands.Details.DeleteContractorContractDetailService;

public class DeleteContractorContractDetailServiceCommandValidator : AbstractValidator<DeleteContractorContractDetailServiceCommand>
{
    public DeleteContractorContractDetailServiceCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithError(ContractorContractDetailErrors.InvalidContractorContractDetailId)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
