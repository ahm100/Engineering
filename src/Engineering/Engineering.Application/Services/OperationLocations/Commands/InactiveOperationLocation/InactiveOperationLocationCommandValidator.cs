namespace Engineering.Application.Services.OperationLocations.Commands.InactiveOperationLocation;

public class InactiveOperationLocationCommandValidator : AbstractValidator<InactiveOperationLocationCommand>
{
    public InactiveOperationLocationCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(OperationLocationErrors.IdIsEmpty);
    }
}