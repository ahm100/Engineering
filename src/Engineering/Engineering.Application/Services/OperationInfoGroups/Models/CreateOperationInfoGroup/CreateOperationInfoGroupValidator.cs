namespace Engineering.Application.Services.OperationInfoGroups.Models.CreateOperationInfoGroup;

public class CreateOperationInfoGroupValidator : AbstractValidator<CreateOperationInfoGroupRequest>
{
    public CreateOperationInfoGroupValidator()
    {
        RuleFor(oo => oo.OperationInfoGroupName).NotEmpty().WithError(OperationInfoGroupErrors.OperationInfoGroupNameIsEmpty);
        RuleFor(oo => oo.OperationInfoGroupCode).NotEmpty().WithError(OperationInfoGroupErrors.OperationInfoGroupCodeIsEmpty);
        RuleFor(oo => oo.OperationInfoGroupCode).Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);
        RuleFor(oo => oo.OperationInfoGroupName).Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);
    }
}
