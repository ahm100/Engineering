namespace Engineering.Application.Services.OperationInfos.Commands.SetOperationInfoPriority;

public class SetOperationInfoPriorityCommandValidator : AbstractValidator<SetOperationInfoPriorityCommand>
{
    public SetOperationInfoPriorityCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(OperationInfoErrors.IdIsEmpty);
    }
}