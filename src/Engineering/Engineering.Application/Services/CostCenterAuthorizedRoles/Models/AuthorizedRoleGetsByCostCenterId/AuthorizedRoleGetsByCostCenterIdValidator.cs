namespace Engineering.Application.Services.CostCenterAuthorizedRoles.Models.AuthorizedRoleGetsByCostCenterId;

public class AuthorizedRoleGetsByCostCenterIdValidator : AbstractValidator<AuthorizedRoleGetsByCostCenterIdRequest>
{
    public AuthorizedRoleGetsByCostCenterIdValidator()
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
