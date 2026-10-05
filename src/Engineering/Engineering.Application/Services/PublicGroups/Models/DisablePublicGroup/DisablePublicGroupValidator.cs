namespace Engineering.Application.Services.PublicGroups.Models.DisablePublicGroup;

public class DisablePublicGroupValidator : AbstractValidator<DisablePublicGroupRequest>
{
    public DisablePublicGroupValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(OperationInfoErrors.NonStandardIdIsEmpty);
    }
}
