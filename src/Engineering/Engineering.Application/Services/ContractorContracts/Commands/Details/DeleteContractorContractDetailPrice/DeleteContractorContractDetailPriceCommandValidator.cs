namespace Engineering.Application.Services.ContractorContracts.Commands.Details.DeleteContractorContractDetailPrice;

public class DeleteContractorContractDetailPriceCommandValidator : AbstractValidator<DeleteContractorContractDetailPriceCommand>
{
    public DeleteContractorContractDetailPriceCommandValidator()
    {
        RuleFor(c => c.Id).NotNull()
            .NotNull().WithError(ContractorContractDetailPriceErrors.InvalidContractorContractDetailPriceId)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
