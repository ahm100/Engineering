
namespace Engineering.Application.Services.ProjectOperations.Models.CreateProjectOperation;

public class CreateProjectOperationValidator : AbstractValidator<CreateProjectOperationRequest>
{
    public CreateProjectOperationValidator()
    {
        RuleFor(oo => oo.OperationInfoId).NotNull().WithError(ProjectOperationErrors.OperationInfoIdIsEmpty);
        RuleFor(oo => oo.Workload).NotNull().WithError(ProjectOperationErrors.WorkloadIsEmpty);
        RuleFor(oo => oo.TolerancePercentage).NotNull().WithError(ProjectOperationErrors.TolerancePercentageIsEmpty);
        RuleFor(oo => oo.UnitOfMeasurementId).NotNull().WithError(ProjectOperationErrors.MeasurementIdIsEmpty);
        RuleFor(oo => oo.ProjectOperationStatus).NotNull().WithError(GlobalErrors.StatusIsNull).IsInEnum().WithError(GlobalErrors.StatusNotInEnum);
    }
}
