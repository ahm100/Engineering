namespace Engineering.Application.Services.OperationInfoDependencies.Models.UpdateOperationInfoDependency;

public class UpdateOperationInfoDependencyValidator : AbstractValidator<UpdateOperationInfoDependencyRequest>
{
    public UpdateOperationInfoDependencyValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(OperationInfoDependencyErrors.IdIsEmpty);
        RuleFor(oo => oo.WorkingDays).NotNull().WithError(OperationInfoDependencyErrors.WorkingDaysIsEmpty);
        RuleFor(oo => oo.RelationId).NotNull().GreaterThanOrEqualTo(1)
            .WithError(OperationInfoDependencyErrors.RelationIdIsEmpty);
        RuleFor(oo => oo.DependencyType).NotNull().WithError(OperationInfoDependencyErrors.DependencyTypeIsEmpty);
    }
}
