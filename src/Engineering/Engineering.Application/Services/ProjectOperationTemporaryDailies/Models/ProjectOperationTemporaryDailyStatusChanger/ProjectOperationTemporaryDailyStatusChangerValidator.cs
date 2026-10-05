
namespace Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.ProjectOperationTemporaryDailyStatusChanger;

public class ProjectOperationTemporaryDailyStatusChangerValidator : AbstractValidator<ProjectOperationTemporaryDailyStatusChangerRequest>
{
    public ProjectOperationTemporaryDailyStatusChangerValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithError(GlobalErrors.IdsIsEmpty)
            .NotNull().WithError(GlobalErrors.IdsIsNull);

        RuleFor(c => c.Id)
            .GreaterThan(0).WithError(GlobalErrors.IdsLessThanOrEqualZero);

        RuleFor(oo => oo.Status)
            .NotNull().WithError(GlobalErrors.StatusIsNull)
            .IsInEnum().WithError(GlobalErrors.StatusNotInEnum);
    }
}
