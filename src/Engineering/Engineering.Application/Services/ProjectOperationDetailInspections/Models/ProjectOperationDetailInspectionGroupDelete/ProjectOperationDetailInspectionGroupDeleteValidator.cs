namespace Engineering.Application.Services.ProjectOperationDetailInspections.Models.ProjectOperationDetailInspectionGroupDelete;

public class ProjectOperationDetailInspectionGroupDeleteValidator : AbstractValidator<ProjectOperationDetailInspectionGroupDeleteRequest>
{
    public ProjectOperationDetailInspectionGroupDeleteValidator()
    {
        RuleFor(c => c.Ids).NotEmpty().WithError(GlobalErrors.IdsIsEmpty).NotNull().WithError(GlobalErrors.IdsIsNull);
        RuleForEach(c => c.Ids).GreaterThan(0).WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
