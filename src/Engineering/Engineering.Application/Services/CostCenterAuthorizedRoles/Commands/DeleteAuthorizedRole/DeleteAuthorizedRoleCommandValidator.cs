namespace Engineering.Application.Services.CostCenterAuthorizedRoles.Commands.DeleteAuthorizedRole;

public class DeleteAuthorizedRoleCommandValidator : AbstractValidator<DeleteAuthorizedRoleCommand>
{
    public DeleteAuthorizedRoleCommandValidator()
    {
        RuleFor(oo => oo.AuthorizedRoleId).NotNull().WithError(CostCenterAuthorizedRoleErrors.AuthorizedRoleIdIsEmpty);
        RuleFor(oo => oo.CostCenterId).NotNull().WithError(CostCenterErrors.IdIsEmpty);
    }
}