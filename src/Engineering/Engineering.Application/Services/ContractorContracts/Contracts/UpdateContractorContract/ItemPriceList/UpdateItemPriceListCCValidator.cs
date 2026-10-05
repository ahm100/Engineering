namespace Engineering.Application.Services.ContractorContracts.Contracts.UpdateContractorContract.ItemPriceList;

public class UpdateItemPriceListCCValidator : AbstractValidator<UpdateItemPriceListCCRequest>
{
    public UpdateItemPriceListCCValidator()
    {
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

        RuleFor(x => x.Id)
        .GreaterThan(0)
        .WithError(
            ContractorContractErrors.InValidContractorContractId);

        When(x => !x.IsDelete &&
                  x.Details is { Count: > 0 }, () =>
                  {
                      RuleFor(x => x.ProjectOperationDetailServiceIds)
                  .NotNull()
                  .WithError(
                      ContractorContractErrors
                          .ProjectOperationDetailServiceIdsIsEmpty)
                  .NotEmpty()
                  .WithError(
                      ContractorContractErrors
                          .ProjectOperationDetailServiceIdsIsEmpty);

                      RuleForEach(x => x.Details!)
                  .ChildRules(detail =>
                {
                    detail.RuleFor(x => x.ProjectOperationId)
                        .GreaterThan(0)
                        .WithError(
                            ContractorContractErrors
                                .InValidProjectOperationServiceId);

                    detail.RuleFor(x => x.ContractCoefficient)
                        .GreaterThan(0)
                        .WithError(
                            ContractorContractErrors
                                .InValidContractCoefficient);
                });
                  });

        When(x => !x.IsDelete &&
                  x.UpdateDetails is { Count: > 0 }, () =>
                  {
                      RuleForEach(x => x.UpdateDetails!)
                  .ChildRules(detail =>
                {
                    detail.RuleFor(x => x.Id)
                        .GreaterThan(0)
                        .WithError(
                            ContractorContractErrors
                                .InValidProjectOperationServiceId);

                    detail.RuleFor(x => x.ProjectOperationId)
                        .GreaterThan(0)
                        .WithError(
                            ContractorContractErrors
                                .InValidProjectOperationServiceId);

                    detail.RuleFor(x => x.ContractCoefficient)
                        .GreaterThan(0)
                        .WithError(
                            ContractorContractErrors
                                .InValidContractCoefficient);
                });
                  });

        When(x => !x.IsDelete &&
                  x.DeleteDetails is { Count: > 0 }, () =>
                  {
                      RuleForEach(x => x.DeleteDetails!)
                  .GreaterThan(0)
                  .WithError(
                      ContractorContractErrors
                          .InValidProjectOperationServiceId);
                  });
    }
}
