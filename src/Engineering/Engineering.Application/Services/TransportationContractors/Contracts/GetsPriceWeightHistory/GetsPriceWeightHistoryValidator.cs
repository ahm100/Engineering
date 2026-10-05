namespace Engineering.Application.Services.TransportationContractors.Contracts.GetsPriceWeightHistory;

public class GetsPriceWeightHistoryValidator : AbstractValidator<GetsPriceWeightHistoryRequest>
{
    public GetsPriceWeightHistoryValidator()
    {
        RuleFor(oo => oo.Id).IsPositive(TransportationContractorPriceWeightCmts.TransportationContractorPriceWeightId);

        RuleFor(oo => oo.PageIndex).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageIndexNotValid)
            .LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexNotValid);

        RuleFor(oo => oo.PageSize).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageSizeNotValid)
            .LessThanOrEqualTo(GlobalErrors.MaxSize).WithError(GlobalErrors.PageSizeNotValid);

        When(oo => oo.PageSize > 0, () =>
        {
            RuleFor(oo => oo.PageIndex).GreaterThanOrEqualTo(GlobalErrors.One).WithError(GlobalErrors.PageIndexRequired)
                .LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexRequired);
        });
    }
}
