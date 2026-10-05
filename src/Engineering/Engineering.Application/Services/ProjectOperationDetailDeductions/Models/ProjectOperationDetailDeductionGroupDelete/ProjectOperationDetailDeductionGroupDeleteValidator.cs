
namespace Engineering.Application.Services.ProjectOperationDetailDeductions.Models.ProjectOperationDetailDeductionGroupDelete;

public class ProjectOperationDetailDeductionGroupDeleteValidator : AbstractValidator<ProjectOperationDetailDeductionGroupDeleteRequest>
{
    public ProjectOperationDetailDeductionGroupDeleteValidator()
    {
        RuleFor(c => c.Ids).NotEmpty().WithError(GlobalErrors.IdsIsEmpty).NotNull().WithError(GlobalErrors.IdsIsNull);
        RuleForEach(c => c.Ids).GreaterThan(0).WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
