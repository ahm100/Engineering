namespace Engineering.Application.Services.OperationInfos.Commands.SetOperationInfoHaveStandard;

public class SetOperationInfoHaveStandardCommandValidator : AbstractValidator<SetOperationInfoHaveStandardCommand>
{
    public SetOperationInfoHaveStandardCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(OperationInfoErrors.IdIsEmpty);
    }
}
