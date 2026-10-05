
namespace Engineering.Application.Services.OperationLocations.Commands.SetPriority;

public class SetOperationLocationPriorityCommandValidator : AbstractValidator<SetOperationLocationPriorityCommand>
{
    public SetOperationLocationPriorityCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(OperationLocationErrors.IdIsEmpty);
        RuleFor(oo => oo.Priority).NotNull().WithError(OperationLocationErrors.PriorityIsEmpty);
    }
}
