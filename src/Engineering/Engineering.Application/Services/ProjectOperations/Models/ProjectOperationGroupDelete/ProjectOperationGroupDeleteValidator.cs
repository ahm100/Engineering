
namespace Engineering.Application.Services.ProjectOperations.Models.ProjectOperationGroupDelete;

public class ProjectOperationGroupDeleteValidator : AbstractValidator<ProjectOperationGroupDeleteRequest>
{
    public ProjectOperationGroupDeleteValidator()
    {
        RuleFor(c => c.Ids).NotEmpty().WithError(GlobalErrors.IdsIsEmpty).NotNull().WithError(GlobalErrors.IdsIsNull);
        RuleForEach(c => c.Ids).GreaterThan(0).WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
