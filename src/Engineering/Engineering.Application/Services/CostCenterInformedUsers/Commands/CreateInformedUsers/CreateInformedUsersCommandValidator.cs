namespace Engineering.Application.Services.CostCenterInformedUsers.Commands.CreateInformedUsers;

public class CreateInformedUsersCommandValidator : AbstractValidator<CreateInformedUsersCommand>
{
    public CreateInformedUsersCommandValidator()
    {
        RuleFor(oo => oo.CostCenter).NotEmpty().WithError(CostCenterErrors.IdIsEmpty);
    }
}
