namespace Engineering.Application.Services.OperationInfoGroups.Commands.InactiveOperationInfoGroup;

public class InactiveOperationInfoGroupCommandValidator : AbstractValidator<InactiveOperationInfoGroupCommand>
{
    public InactiveOperationInfoGroupCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(OperationInfoGroupErrors.IdIsEmpty);
    }
}