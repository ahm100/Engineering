namespace Engineering.Application.Services.CostCenterInformedUsers.Models.CreateInformedUser;

public class CreateInformedUserValidator : AbstractValidator<CreateInformedUserRequest>
{
    public CreateInformedUserValidator()
    {
        RuleFor(oo => oo.EmployeeId)
            .IsPositive(CCenterCmts.EmployeeId);
        RuleFor(oo => oo.CostCenterId)
            .IsPositive(GlobalCmts.CostCenterId);
    }
}
