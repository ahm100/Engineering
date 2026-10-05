namespace Engineering.Application.Services.CostCenterAuthorizedRoles.Commands.CreateAuthorizedRoles;

public class CreateAuthorizedRolesCommandValidator : AbstractValidator<CreateAuthorizedRolesCommand>
{
    public CreateAuthorizedRolesCommandValidator()
    {
        RuleFor(oo => oo.CostCenter).NotEmpty().WithError(CostCenterErrors.IdIsEmpty);
    }
}
