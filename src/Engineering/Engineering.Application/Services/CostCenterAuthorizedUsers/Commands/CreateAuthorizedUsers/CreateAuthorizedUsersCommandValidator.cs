namespace Engineering.Application.Services.CostCenterAuthorizedUsers.Commands.CreateAuthorizedUsers;

public class CreateAuthorizedUsersCommandValidator : AbstractValidator<CreateAuthorizedUsersCommand>
{
    public CreateAuthorizedUsersCommandValidator()
    {
        RuleFor(oo => oo.CostCenter).NotEmpty().WithError(CostCenterErrors.IdIsEmpty);
    }
}
