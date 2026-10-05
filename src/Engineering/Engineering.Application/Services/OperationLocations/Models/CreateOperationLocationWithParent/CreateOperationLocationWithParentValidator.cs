
namespace Engineering.Application.Services.OperationLocations.Models.CreateOperationLocationWithParent;

public class CreateOperationLocationWithParentValidator : AbstractValidator<CreateOperationLocationWithParentRequest>
{
    public CreateOperationLocationWithParentValidator()
    {
        RuleFor(oo => oo.ParentId).NotNull().WithError(OperationLocationErrors.ParentIdIsEmpty);
        RuleFor(oo => oo.PrivateCode).NotNull().WithError(OperationLocationErrors.PrivateCodeIsEmpty);
        RuleFor(oo => oo.PrivateName).NotNull().WithError(OperationLocationErrors.PrivateNameIsEmpty);
        RuleFor(oo => oo.Priority).NotNull().WithError(OperationLocationErrors.PriorityIsEmpty);
        RuleFor(oo => oo.IsActive).NotNull().WithError(OperationLocationErrors.IsActiveIsEmpty);
        RuleFor(oo => oo.PrivateCode).Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);
        RuleFor(oo => oo.PrivateName).Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);
    }
}
