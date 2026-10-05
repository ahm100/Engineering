namespace Engineering.Application.Services.OperationInfoGroups.Commands.DisableOperationInfoGroup;

public class DisableOperationInfoGroupCommandValidator : AbstractValidator<DisableOperationInfoGroupCommand>
{
    public DisableOperationInfoGroupCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(OperationInfoGroupErrors.IdIsEmpty);
    }
}