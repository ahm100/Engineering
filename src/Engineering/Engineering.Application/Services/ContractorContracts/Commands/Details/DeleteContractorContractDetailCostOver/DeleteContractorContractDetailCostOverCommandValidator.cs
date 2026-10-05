namespace Engineering.Application.Services.ContractorContracts.Commands.Details.DeleteContractorContractDetailCostOver;

public class DeleteContractorContractDetailCostOverCommandValidator : AbstractValidator<DeleteContractorContractDetailCostOverCommand>
{
    public DeleteContractorContractDetailCostOverCommandValidator()
    {
        RuleFor(c => c.Id).NotNull()
            .NotNull().WithError(ContractorContractDetailPriceErrors.ContractorContractDetailPriceWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
