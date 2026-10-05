namespace Engineering.Application.Services.OperationInfoGroups.Models.DisableOperationInfoGroup;

public class DisableOperationInfoGroupValidator : AbstractValidator<DisableOperationInfoGroupRequest>
{
    public DisableOperationInfoGroupValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(OperationInfoGroupErrors.IdIsEmpty);
    }
}
