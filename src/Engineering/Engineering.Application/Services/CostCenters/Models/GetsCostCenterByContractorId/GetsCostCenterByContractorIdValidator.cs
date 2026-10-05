namespace Engineering.Application.Services.CostCenters.Models.GetsCostCenterByContractorId;

public class GetsCostCenterByContractorIdValidator : AbstractValidator<GetsCostCenterByContractorIdRequest>
{
    public GetsCostCenterByContractorIdValidator()
    {
        RuleFor(oo => oo.ContractorId)
            .IsPositive(GlobalCmts.ContractorId);
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
