namespace Engineering.Application.Services.OperationInfos.Commands.ActiveOperationInfo;

public class ActiveOperationInfoCommandValidator : AbstractValidator<ActiveOperationInfoCommand>
{
    public ActiveOperationInfoCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(OperationInfoErrors.IdIsEmpty);
    }
}