namespace Engineering.Application.Services.ContractorContracts.Commands.Details.UpdateContractorContractDetailPrice;

public class UpdateContractorContractDetailPriceCommandValidator : AbstractValidator<UpdateContractorContractDetailPriceCommand>
{
    public UpdateContractorContractDetailPriceCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithError(ContractorContractErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(ContractorContractErrors.IdIsEmpty);

        RuleFor(c => c.CurrencyId)
            .NotNull().WithError(ContractorContractErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(ContractorContractErrors.CurrencyIdIsEmpty);

        RuleFor(c => c.StartDate)
            .NotEmpty().WithError(ContractorContractErrors.StartDateIsEmpty);

        RuleFor(c => c.EndDate)
            .NotEmpty().WithError(ContractorContractErrors.EndDateIsEmpty);

        RuleFor(c => c.Price)
            .NotNull().WithError(ContractorContractErrors.PriceIsEmpty);
    }
}
