namespace Engineering.Application.Services.OperationInfoGroups.Models.UpdateOperationInfoGroup;

public class UpdateOperationInfoGroupValidator : AbstractValidator<UpdateOperationInfoGroupRequest>
{
    public UpdateOperationInfoGroupValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(OperationInfoGroupErrors.IdIsEmpty);
        RuleFor(oo => oo.OperationInfoGroupName).NotEmpty().WithError(OperationInfoGroupErrors.OperationInfoGroupNameIsEmpty);
        RuleFor(oo => oo.OperationInfoGroupCode).NotEmpty().WithError(OperationInfoGroupErrors.OperationInfoGroupCodeIsEmpty);
        RuleFor(oo => oo.OperationInfoGroupCode).Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);
        RuleFor(oo => oo.OperationInfoGroupName).Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);
    }
}
