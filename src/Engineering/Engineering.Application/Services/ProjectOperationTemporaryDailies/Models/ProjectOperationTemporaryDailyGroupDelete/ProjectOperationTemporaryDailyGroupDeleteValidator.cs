namespace Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.ProjectOperationTemporaryDailyGroupDelete;

public class ProjectOperationTemporaryDailyGroupDeleteValidator : AbstractValidator<ProjectOperationTemporaryDailyGroupDeleteRequest>
{
    public ProjectOperationTemporaryDailyGroupDeleteValidator()
    {
        RuleFor(c => c.Ids).NotEmpty().WithError(GlobalErrors.IdsIsEmpty).NotNull().WithError(GlobalErrors.IdsIsNull);
        RuleForEach(c => c.Ids).GreaterThan(0).WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
