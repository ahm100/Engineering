
namespace Engineering.Application.Services.OperationInfoDependencies.Commands.CreateOperationInfoDependency;

public class CreateOperationInfoDependencyCommandValidator : AbstractValidator<CreateOperationInfoDependencyCommand>
{
    public CreateOperationInfoDependencyCommandValidator()
    {
        RuleFor(oo => oo.RelationId).NotNull().GreaterThanOrEqualTo(1)
            .WithError(OperationInfoDependencyErrors.RelationIdIsEmpty);
        RuleFor(oo => oo.DependencyType).NotNull().WithError(OperationInfoDependencyErrors.DependencyTypeIsEmpty);
        RuleFor(oo => oo.OperationInfo).NotNull().WithError(OperationInfoDependencyErrors.OperationInfoIdIsEmpty);
        RuleFor(oo => oo.WorkingDays).NotNull().WithError(OperationInfoDependencyErrors.WorkingDaysIsEmpty);
    }
}
