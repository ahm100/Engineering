namespace Engineering.Application.Services.ContractorContracts.Contracts.GetServicePriceHistory;

public class GetServicePriceHistoryValidator : AbstractValidator<GetServicePriceHistoryRequest>
{
    public GetServicePriceHistoryValidator()
    {
        RuleFor(c => c.ServiceInfoId)
            .IsPositive(GlobalCmts.ServiceId);

        RuleFor(c => c.PageIndex)
            .PageIndexZero(GlobalCmts.PageIndex);
        RuleFor(c => c.PageSize)
            .PageSizeZero(GlobalCmts.PageSize);
        When(v => v.PageSize > 0, () =>
        {
            RuleFor(c => c.PageIndex)
                .PageIndexOne(GlobalCmts.PageIndex);
        });
    }
}
