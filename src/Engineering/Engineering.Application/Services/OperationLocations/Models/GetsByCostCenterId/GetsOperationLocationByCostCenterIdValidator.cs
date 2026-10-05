namespace Engineering.Application.Services.OperationLocations.Models.GetsByCostCenterId;

public class GetsOperationLocationByCostCenterIdValidator : AbstractValidator<GetsOperationLocationByCostCenterIdRequest>
{
    public GetsOperationLocationByCostCenterIdValidator()
    {
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
