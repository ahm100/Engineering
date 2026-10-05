
namespace Engineering.Application.Services.OperationLocations.Models.CreateOperationLocationWithCostCenter;

public class CreateOperationLocationWithCostCenterValidator : AbstractValidator<CreateOperationLocationWithCostCenterRequest>
{
    public CreateOperationLocationWithCostCenterValidator()
    {
        RuleFor(oo => oo.PrivateCode).NotNull().WithError(OperationLocationErrors.PrivateCodeIsEmpty);
        RuleFor(oo => oo.PrivateName).NotNull().WithError(OperationLocationErrors.PrivateNameIsEmpty);
        RuleFor(oo => oo.IsActive).NotNull().WithError(OperationLocationErrors.IsActiveIsEmpty);
        RuleFor(oo => oo.PrivateCode).Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);
        RuleFor(oo => oo.PrivateName).Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);
    }
}
