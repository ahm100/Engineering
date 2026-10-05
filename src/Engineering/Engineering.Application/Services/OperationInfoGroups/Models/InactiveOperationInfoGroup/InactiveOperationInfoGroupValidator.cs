namespace Engineering.Application.Services.OperationInfoGroups.Models.InactiveOperationInfoGroup;

public class InactiveOperationInfoGroupValidator : AbstractValidator<InactiveOperationInfoGroupRequest>
{
    public InactiveOperationInfoGroupValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(OperationInfoGroupErrors.IdIsEmpty);
    }
}
