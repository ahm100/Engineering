namespace Engineering.Application.Services.CostCenterAuthorizedRoles.Models.CreateAuthorizedRoles;

public class CreateAuthorizedRolesValidator : AbstractValidator<CreateAuthorizedRolesRequest>
{
    public CreateAuthorizedRolesValidator()
    {
        RuleFor(oo => oo.CostCenterId)
            .IsPositive(GlobalCmts.CostCenterId);
    }
}
