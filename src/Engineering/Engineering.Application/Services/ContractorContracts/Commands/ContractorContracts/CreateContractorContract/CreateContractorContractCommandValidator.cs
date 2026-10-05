namespace Engineering.Application.Services.ContractorContracts.Commands.ContractorContracts.CreateContractorContract;

public class CreateContractorContractCommandValidator : AbstractValidator<CreateContractorContractCommand>
{
    public CreateContractorContractCommandValidator()
    {
        RuleFor(oo => oo.ContractorContractHeader)
            .NotNull().WithError(ContractorContractErrors.TypeIsEmpty);

        RuleFor(oo => oo.ContractorContractType)
            .NotNull().WithError(ContractorContractErrors.TypeIsEmpty);

        RuleFor(oo => oo.TotalAmount)
            .NotNull().WithError(ContractorContractErrors.InValidTotalAmount);

        RuleFor(oo => oo.StartDate)
            .NotEmpty().WithError(GlobalErrors.StartDateIsNull);

        RuleFor(oo => oo.EndDate)
            .NotEmpty().WithError(GlobalErrors.EndDateIsNull);

        RuleFor(oo => oo.StartDate.Date)
            .LessThanOrEqualTo(oo => oo.EndDate.Date).WithError(GlobalErrors.StartDateCanNotBigerToEndDate);

        RuleFor(oo => oo.EndDate.Date)
            .GreaterThanOrEqualTo(oo => oo.StartDate.Date).WithError(GlobalErrors.EndDateCanNotSmallerToStartDate);

        When(oo => oo.PercentageDoingJobWell != null, () =>
        {
            RuleFor(oo => oo.PercentageDoingJobWell).GreaterThanOrEqualTo(0).WithError(GlobalErrors.PricesMusbeGreaterThanOrEqualZiro);
        });

        When(oo => oo.DoingJobWellAmount != null, () =>
        {
            RuleFor(oo => oo.DoingJobWellAmount).GreaterThanOrEqualTo(0).WithError(GlobalErrors.PricesMusbeGreaterThanOrEqualZiro);
        });

        When(oo => oo.PercentageAdvancePayment != null, () =>
        {
            RuleFor(oo => oo.PercentageAdvancePayment).GreaterThanOrEqualTo(0).WithError(GlobalErrors.PricesMusbeGreaterThanOrEqualZiro);
        });

        When(oo => oo.AdvancePaymentAmount != null, () =>
        {
            RuleFor(oo => oo.AdvancePaymentAmount).GreaterThanOrEqualTo(0).WithError(GlobalErrors.PricesMusbeGreaterThanOrEqualZiro);
        });

        When(oo => oo.DailyLatenessPenalty != null, () =>
        {
            RuleFor(oo => oo.DailyLatenessPenalty).GreaterThanOrEqualTo(0).WithError(GlobalErrors.PricesMusbeGreaterThanOrEqualZiro);
        });

    }
}
