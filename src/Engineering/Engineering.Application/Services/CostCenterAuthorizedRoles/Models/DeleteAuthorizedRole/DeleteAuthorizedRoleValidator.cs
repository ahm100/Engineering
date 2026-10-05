namespace Engineering.Application.Services.CostCenterAuthorizedRoles.Models.DeleteAuthorizedRole;

public class DeleteAuthorizedRoleValidator : AbstractValidator<DeleteAuthorizedRoleRequest>
{
    public DeleteAuthorizedRoleValidator()
    {
        RuleFor(oo => oo.AuthorizedRoleId)
            .IsPositive(CCenterCmts.AuthorizedRoleId);
        RuleFor(oo => oo.CostCenterId)
            .IsPositive(GlobalCmts.CostCenterId);
    }
}
