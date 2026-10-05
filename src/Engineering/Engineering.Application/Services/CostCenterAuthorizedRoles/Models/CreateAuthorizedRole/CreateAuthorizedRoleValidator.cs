namespace Engineering.Application.Services.CostCenterAuthorizedRoles.Models.CreateAuthorizedRole;

public class CreateAuthorizedRoleValidator : AbstractValidator<CreateAuthorizedRoleRequest>
{
    public CreateAuthorizedRoleValidator()
    {
        RuleFor(oo => oo.AuthorizedRoleId)
            .IsPositive(CCenterCmts.AuthorizedRoleId);
        RuleFor(oo => oo.CostCenterId)
            .IsPositive(GlobalCmts.CostCenterId);
    }
}
