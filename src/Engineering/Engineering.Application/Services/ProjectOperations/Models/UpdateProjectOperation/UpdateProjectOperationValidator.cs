
namespace Engineering.Application.Services.ProjectOperations.Models.UpdateProjectOperation;

public class UpdateProjectOperationValidator : AbstractValidator<UpdateProjectOperationRequest>
{
    public UpdateProjectOperationValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectOperationErrors.IdIsEmpty);
        RuleFor(oo => oo.Priority).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectOperationErrors.PriorityCanNot0);
        RuleFor(oo => oo.Workload).NotNull().WithError(ProjectOperationErrors.WorkloadIsEmpty);
        RuleFor(oo => oo.TolerancePercentage).NotNull().WithError(ProjectOperationErrors.TolerancePercentageIsEmpty);
        RuleFor(oo => oo.OperationInfoId).NotNull().WithError(ProjectOperationErrors.OperationInfoIdIsEmpty);
        RuleFor(oo => oo.Status).NotNull().WithError(GlobalErrors.StatusIsNull).IsInEnum().WithError(GlobalErrors.StatusNotInEnum);
    }
}
