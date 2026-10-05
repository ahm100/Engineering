namespace Engineering.Application.Services.OperationLocations.Commands.DisableOperationLocation;

public class DisableOperationLocationCommandValidator : AbstractValidator<DisableOperationLocationCommand>
{
    public DisableOperationLocationCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(OperationLocationErrors.IdIsEmpty);
    }
}