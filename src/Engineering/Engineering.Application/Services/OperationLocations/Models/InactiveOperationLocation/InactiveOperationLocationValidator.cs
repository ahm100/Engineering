namespace Engineering.Application.Services.OperationLocations.Models.InactiveOperationLocation;

public class InactiveOperationLocationValidator : AbstractValidator<InactiveOperationLocationRequest>
{
    public InactiveOperationLocationValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(OperationLocationErrors.IdIsEmpty);
    }
}
