namespace Engineering.Application.Services.ProjectOperationDetailSchedulings.Models.UpdateProjectOperationDetailDatesByDate;

public class UpdateProjectOperationDetailDatesByDateValidator : AbstractValidator<UpdateProjectOperationDetailDatesByDateRequest>
{
    public UpdateProjectOperationDetailDatesByDateValidator()
    {
        RuleFor(oo => oo.CostCenterId).NotNull().WithError(CostCenterErrors.IdIsEmpty);
        RuleFor(oo => oo.ProjectId).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ProjectErrors.IdIsEmpty);
        RuleFor(oo => oo.OperationInfoIds).NotEmpty().WithError(OperationInfoErrors.UnValidIds);
        RuleFor(oo => oo.OperationLocationIds).NotEmpty().WithError(OperationLocationErrors.UnValidIds);
        RuleFor(oo => oo.StartDate).NotEmpty().WithError(ProjectOperationDetailErrors.InValidStartDate);
        RuleFor(oo => oo.EndDate).NotEmpty().WithError(ProjectOperationDetailErrors.InValidEndDate);
        RuleFor(x => x).Must(x => x.EndDate == default(DateTime) || x.StartDate == default(DateTime) || x.EndDate > x.StartDate)
        .WithError(ProjectOperationDetailErrors.DateTimeNotValid);
    }
}
