namespace Engineering.Application.Services.OperationLocations.Models.SetPriority;

public class SetOperationLocationPriorityValidator : AbstractValidator<SetOperationLocationPriorityRequest>
{
    public SetOperationLocationPriorityValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(OperationLocationErrors.IdIsEmpty);
        RuleFor(oo => oo.Priority).NotNull().WithError(OperationLocationErrors.PriorityIsEmpty);
    }
}
