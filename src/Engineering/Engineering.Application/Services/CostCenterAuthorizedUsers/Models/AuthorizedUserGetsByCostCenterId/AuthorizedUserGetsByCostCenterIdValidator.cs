namespace Engineering.Application.Services.CostCenterAuthorizedUsers.Models.AuthorizedUserGetsByCostCenterId;

public class AuthorizedUserGetsByCostCenterIdValidator : AbstractValidator<AuthorizedUserGetsByCostCenterIdRequest>
{
    public AuthorizedUserGetsByCostCenterIdValidator()
    {
        RuleFor(oo => oo.CostCenterId)
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
