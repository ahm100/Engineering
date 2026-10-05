namespace Engineering.Application.Services.OperationLocations.Models.DisableOperationLocation;

public class DisableOperationLocationValidator : AbstractValidator<DisableOperationLocationRequest>
{
    public DisableOperationLocationValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(OperationLocationErrors.IdIsEmpty);
    }
}
