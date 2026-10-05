namespace Engineering.Application.Services.ContractorContracts.Contracts.UpdateContractorContract;

public class UpdateProfessionalWorkdayCCValidator : AbstractValidator<UpdateProfessionalWorkdayCCRequest>
{
    public UpdateProfessionalWorkdayCCValidator()
    {
        RuleFor(oo => oo.TotalAmount)
            .NotNull().WithError(ContractorContractErrors.InValidTotalAmount);

        RuleFor(oo => oo.StartDate)
            .IsDate(GlobalCmts.StartDate)
            .LessThanPropertyDate(x => x.EndDate
                , GlobalCmts.StartDate
                , GlobalCmts.EndDate);

        RuleFor(oo => oo.EndDate)
            .IsDate(GlobalCmts.EndDate)
            .GreaterThanPropertyDate(x => x.StartDate
                , GlobalCmts.StartDate
                , GlobalCmts.EndDate);

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
