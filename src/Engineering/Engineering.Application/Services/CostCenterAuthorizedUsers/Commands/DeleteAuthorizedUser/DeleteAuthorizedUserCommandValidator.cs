namespace Engineering.Application.Services.CostCenterAuthorizedUsers.Commands.DeleteAuthorizedUser;

public class DeleteAuthorizedUserCommandValidator : AbstractValidator<DeleteAuthorizedUserCommand>
{
    public DeleteAuthorizedUserCommandValidator()
    {
        RuleFor(oo => oo.CostCenterId).NotNull().WithError(CostCenterErrors.IdIsEmpty);
        RuleFor(oo => oo.AuthorizedUserId).NotNull().WithError(CostCenterAuthorizedUserErrors.UserIdIsEmpty);
    }
}