namespace Engineering.Application.Services.CostCenterInformedUsers.Commands.DeleteInformedUser;

public class DeleteInformedUserCommandValidator : AbstractValidator<DeleteInformedUserCommand>
{
    public DeleteInformedUserCommandValidator()
    {
        RuleFor(oo => oo.CostCenterId).NotNull().WithError(CostCenterErrors.IdIsEmpty);
        RuleFor(oo => oo.EmployeeId).NotNull().WithError(CostCenterInformedUserErrors.EmployeeIdIsEmpty);
    }
}