namespace Engineering.Application.Services.CostCenterAuthorizedUsers.Commands.CreateAuthorizedUser;

public class CreateAuthorizedUserCommandValidator : AbstractValidator<CreateAuthorizedUserCommand>
{
    public CreateAuthorizedUserCommandValidator()
    {
        RuleFor(oo => oo.CostCenter).NotEmpty().WithError(CostCenterErrors.IdIsEmpty);
        RuleFor(oo => oo.AuthorizedUserId).NotNull().WithError(CostCenterAuthorizedUserErrors.UserIdIsEmpty);
    }
}