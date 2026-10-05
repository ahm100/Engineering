namespace Engineering.Application.Services.ProjectOperationDetailSchedulings.Models.UpdateProjectOperationDetailDatesByDay;

public class UpdateProjectOperationDetailDatesByDayValidator : AbstractValidator<UpdateProjectOperationDetailDatesByDayRequest>
{
    public UpdateProjectOperationDetailDatesByDayValidator()
    {
        RuleFor(oo => oo.CostCenterId).NotNull().WithError(CostCenterErrors.IdIsEmpty);
        RuleFor(oo => oo.ProjectId).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ProjectErrors.IdIsEmpty);
        RuleForEach(oo => oo.OperationInfoIds).GreaterThanOrEqualTo(1).NotEmpty().WithError(OperationInfoErrors.UnValidIds);
        RuleForEach(oo => oo.OperationLocationIds).GreaterThanOrEqualTo(1).NotEmpty().WithError(OperationLocationErrors.UnValidIds);
        RuleFor(oo => oo.CountDay).NotEqual(0).WithError(ProjectOperationDetailErrors.InValidCountDay);
        RuleFor(oo => oo.DependencyType).IsInEnum().WithError(OperationInfoDependencyErrors.InValidDependencyType);
    }
}
