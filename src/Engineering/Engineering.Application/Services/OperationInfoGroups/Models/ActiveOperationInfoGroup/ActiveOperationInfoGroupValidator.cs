namespace Engineering.Application.Services.OperationInfoGroups.Models.ActiveOperationInfoGroup;

public class ActiveOperationInfoGroupValidator : AbstractValidator<ActiveOperationInfoGroupRequest>
{
    public ActiveOperationInfoGroupValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(OperationInfoGroupErrors.IdIsEmpty);
    }
}
