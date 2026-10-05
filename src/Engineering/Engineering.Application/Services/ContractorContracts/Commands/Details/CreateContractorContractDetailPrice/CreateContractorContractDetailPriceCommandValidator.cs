namespace Engineering.Application.Services.ContractorContracts.Commands.Details.CreateContractorContractDetailPrice;

public class CreateContractorContractDetailPriceCommandValidator : AbstractValidator<CreateContractorContractDetailPriceCommand>
{
    public CreateContractorContractDetailPriceCommandValidator()
    {
        RuleFor(c => c.ContractorContractDetail)
            .NotNull().WithError(ContractorContractErrors.DetailIsEmpty);

        RuleFor(oo => oo.StartDate)
            .NotEmpty().WithError(GlobalErrors.StartDateIsNull);

        RuleFor(oo => oo.EndDate)
            .NotEmpty().WithError(GlobalErrors.EndDateIsNull);

        RuleFor(oo => oo.StartDate.Date)
            .LessThanOrEqualTo(oo => oo.EndDate.Date).WithError(GlobalErrors.StartDateCanNotBigerToEndDate);

        RuleFor(oo => oo.EndDate.Date)
            .GreaterThanOrEqualTo(oo => oo.StartDate.Date).WithError(GlobalErrors.EndDateCanNotSmallerToStartDate);

        RuleFor(c => c.CurrencyId)
            .NotNull().WithError(ContractorContractErrors.CurrencyIdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);

        RuleFor(c => c.Price)
            .NotNull().WithError(ContractorContractErrors.PriceIsEmpty);
    }
}
