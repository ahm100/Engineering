
namespace Engineering.Application.Services.ProjectOperationDetails.Models.ProjectOperationDetailGroupDelete;

public class ProjectOperationDetailGroupDeleteValidator : AbstractValidator<ProjectOperationDetailGroupDeleteRequest>
{
    public ProjectOperationDetailGroupDeleteValidator()
    {
        RuleFor(c => c.Ids)
            .NotEmpty().WithError(GlobalErrors.IdsIsEmpty)
            .NotNull().WithError(GlobalErrors.IdsIsNull);

        RuleForEach(c => c.Ids)
            .GreaterThan(0).WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
