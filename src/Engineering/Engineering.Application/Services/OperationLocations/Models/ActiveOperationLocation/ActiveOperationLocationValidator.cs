
namespace Engineering.Application.Services.OperationLocations.Models.ActiveOperationLocation;

public class ActiveOperationLocationValidator : AbstractValidator<ActiveOperationLocationRequest>
{
    public ActiveOperationLocationValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(OperationLocationErrors.IdIsEmpty);
    }
}
