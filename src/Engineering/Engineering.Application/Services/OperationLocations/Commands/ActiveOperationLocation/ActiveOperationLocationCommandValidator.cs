namespace Engineering.Application.Services.OperationLocations.Commands.ActiveOperationLocation;

public class ActiveOperationLocationCommandValidator : AbstractValidator<ActiveOperationLocationCommand>
{
    public ActiveOperationLocationCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(OperationLocationErrors.IdIsEmpty);
    }
}