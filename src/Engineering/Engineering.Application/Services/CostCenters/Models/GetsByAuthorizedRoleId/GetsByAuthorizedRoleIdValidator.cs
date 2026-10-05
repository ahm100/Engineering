
namespace Engineering.Application.Services.CostCenters.Models.GetsByAuthorizedRoleId;

public class GetsByAuthorizedRoleIdValidator : AbstractValidator<GetsByAuthorizedRoleIdRequest>
{
    public GetsByAuthorizedRoleIdValidator()
    {
        RuleFor(oo => oo.RoleId)
            .IsPositive(CCenterCmts.AuthorizedRoleId);
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
