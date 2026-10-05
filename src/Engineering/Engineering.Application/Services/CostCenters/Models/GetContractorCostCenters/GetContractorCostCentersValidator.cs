namespace Engineering.Application.Services.CostCenters.Models.GetContractorCostCenters;

public class GetContractorCostCentersValidator : AbstractValidator<GetContractorCostCentersRequest>
{
    public GetContractorCostCentersValidator()
    {
        RuleFor(oo => oo.ContractorId).
            IsPositive(GlobalCmts.ContractorId);
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
