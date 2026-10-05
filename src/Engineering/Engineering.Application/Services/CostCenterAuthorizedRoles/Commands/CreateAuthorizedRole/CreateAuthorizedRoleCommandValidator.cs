namespace Engineering.Application.Services.CostCenterAuthorizedRoles.Commands.CreateAuthorizedRole;

public class CreateAuthorizedRoleCommandValidator : AbstractValidator<CreateAuthorizedRoleCommand>
{
    public CreateAuthorizedRoleCommandValidator()
    {
        RuleFor(oo => oo.AuthorizedRoleId).NotNull().WithError(CostCenterAuthorizedRoleErrors.AuthorizedRoleIdIsEmpty);
        RuleFor(oo => oo.CostCenter).NotEmpty().WithError(CostCenterErrors.IdIsEmpty);
    }
}