namespace Engineering.Application.Services.OperationInfos.Commands.DisableOperationInfo;

public class DisableOperationInfoCommandValidator : AbstractValidator<DisableOperationInfoCommand>
{
    public DisableOperationInfoCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(OperationInfoErrors.IdIsEmpty);
    }
}