namespace Engineering.Application.Services.CostCenters.Queries.GetContractorCostCenters;

public class GetContractorCostCentersQueryValidator : AbstractValidator<GetContractorCostCentersQuery>
{
    public GetContractorCostCentersQueryValidator()
    {
        RuleFor(c => c.ContractorId)
            .IsPositive(GlobalCmts.Id);
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