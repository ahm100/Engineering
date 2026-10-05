namespace Engineering.Application.Services.OperationLocations.Commands.UpdateOperationLocation;

public class UpdateOperationLocationCommandValidator : AbstractValidator<UpdateOperationLocationCommand>
{
    public UpdateOperationLocationCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(OperationLocationErrors.IdIsEmpty);
        RuleFor(oo => oo.IsActive).NotNull().WithError(OperationLocationErrors.IsActiveIsEmpty);
    }
}