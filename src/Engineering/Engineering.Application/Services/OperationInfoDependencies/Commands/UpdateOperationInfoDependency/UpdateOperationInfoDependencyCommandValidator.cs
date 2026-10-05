
namespace Engineering.Application.Services.OperationInfoDependencies.Commands.UpdateOperationInfoDependency;

public class UpdateOperationInfoDependencyCommandValidator : AbstractValidator<UpdateOperationInfoDependencyCommand>
{
    public UpdateOperationInfoDependencyCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(OperationInfoDependencyErrors.IdIsEmpty);
        RuleFor(oo => oo.DependencyType).NotNull().WithError(OperationInfoDependencyErrors.DependencyTypeIsEmpty);
        RuleFor(oo => oo.OperationInfo).NotNull().WithError(OperationInfoDependencyErrors.OperationInfoIdIsEmpty);
        RuleFor(oo => oo.WorkingDays).NotNull().WithError(OperationInfoDependencyErrors.WorkingDaysIsEmpty);
    }
}