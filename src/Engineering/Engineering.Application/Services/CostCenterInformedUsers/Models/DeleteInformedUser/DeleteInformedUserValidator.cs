namespace Engineering.Application.Services.CostCenterInformedUsers.Models.DeleteInformedUser;

public class DeleteInformedUserValidator : AbstractValidator<DeleteInformedUserRequest>
{
    public DeleteInformedUserValidator()
    {
        RuleFor(oo => oo.EmployeeId)
            .IsPositive(CCenterCmts.EmployeeId);
        RuleFor(oo => oo.CostCenterId)
            .IsPositive(GlobalCmts.CostCenterId);
    }
}
