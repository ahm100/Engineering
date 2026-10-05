
namespace Engineering.Application.Services.ProjectOperations.Models.GetFltrBasePricedPOs;

public class GetFltrBasePricedPOsValidator : AbstractValidator<GetFltrBasePricedPOsRequest>
{
    public GetFltrBasePricedPOsValidator()
    {
        RuleFor(c => c.CostCenterId)
            .IsPositive(GlobalCmts.CostCenterId);

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
