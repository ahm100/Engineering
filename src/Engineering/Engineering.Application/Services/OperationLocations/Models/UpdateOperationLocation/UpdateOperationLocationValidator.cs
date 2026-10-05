namespace Engineering.Application.Services.OperationLocations.Models.UpdateOperationLocation;

public class UpdateOperationLocationValidator : AbstractValidator<UpdateOperationLocationRequest>
{
    public UpdateOperationLocationValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(OperationLocationErrors.IdIsEmpty);
        RuleFor(oo => oo.Priority).NotNull().WithError(OperationLocationErrors.PriorityIsEmpty);
        RuleFor(oo => oo.IsActive).NotNull().WithError(OperationLocationErrors.IsActiveIsEmpty);
        RuleFor(oo => oo.PrivateCode).Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);
        RuleFor(oo => oo.PrivateName).Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);
    }
}
