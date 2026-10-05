namespace Engineering.Application.Services.OperationInfoDependencies.Commands.DisableOperationInfoDependency;

public class DisableOperationInfoDependencyCommandValidator : AbstractValidator<DisableOperationInfoDependencyCommand>
{
    public DisableOperationInfoDependencyCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(OperationInfoErrors.IdIsEmpty);
    }
}