namespace Engineering.Application.Services.OperationInfoGroups.Commands.ActiveOperationInfoGroup;

public class ActiveOperationInfoGroupCommandValidator : AbstractValidator<ActiveOperationInfoGroupCommand>
{
    public ActiveOperationInfoGroupCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(OperationInfoGroupErrors.IdIsEmpty);
    }
}