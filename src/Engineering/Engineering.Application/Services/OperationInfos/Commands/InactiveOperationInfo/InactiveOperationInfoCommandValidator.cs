namespace Engineering.Application.Services.OperationInfos.Commands.InactiveOperationInfo;

public class InactiveOperationInfoCommandValidator : AbstractValidator<InactiveOperationInfoCommand>
{
    public InactiveOperationInfoCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(OperationInfoErrors.IdIsEmpty);
    }
}