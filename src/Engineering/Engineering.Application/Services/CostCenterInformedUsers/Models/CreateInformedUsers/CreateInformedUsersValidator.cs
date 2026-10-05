namespace Engineering.Application.Services.CostCenterInformedUsers.Models.CreateInformedUsers;

public class CreateInformedUsersValidator : AbstractValidator<CreateInformedUsersRequest>
{
    public CreateInformedUsersValidator()
    {
        RuleFor(oo => oo.CostCenterId)
            .IsPositive(GlobalCmts.CostCenterId);
    }
}
