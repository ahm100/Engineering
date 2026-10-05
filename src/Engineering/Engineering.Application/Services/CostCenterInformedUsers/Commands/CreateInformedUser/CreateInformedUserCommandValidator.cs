namespace Engineering.Application.Services.CostCenterInformedUsers.Commands.CreateInformedUser;

public class CreateInformedUserCommandValidator : AbstractValidator<CreateInformedUserCommand>
{
    public CreateInformedUserCommandValidator()
    {
        RuleFor(oo => oo.CostCenter).NotEmpty().WithError(CostCenterErrors.IdIsEmpty);
        RuleFor(oo => oo.EmployeeId).NotNull().WithError(CostCenterInformedUserErrors.EmployeeIdIsEmpty);
    }
}