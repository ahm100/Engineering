namespace Engineering.Application.Services.OperationInfos.Models.SetOperationInfoPriority;

public class SetOperationInfoPriorityValidator : AbstractValidator<SetOperationInfoPriorityRequest>
{
    public SetOperationInfoPriorityValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(OperationInfoErrors.IdIsEmpty);
    }
}
