
namespace Engineering.Application.Services.ProjectOperations.Models.GroupProjectOperationStatusChanger;

public class GroupProjectOperationStatusChangerValidator : AbstractValidator<GroupProjectOperationStatusChangerRequest>
{
    public GroupProjectOperationStatusChangerValidator()
    {
        RuleFor(c => c.Ids)
            .NotEmpty().WithError(GlobalErrors.IdsIsEmpty)
            .NotNull().WithError(GlobalErrors.IdsIsNull);

        RuleForEach(c => c.Ids)
            .GreaterThan(0).WithError(GlobalErrors.IdsLessThanOrEqualZero);

        RuleFor(oo => oo.Status)
            .NotNull().WithError(GlobalErrors.StatusIsNull)
            .IsInEnum().WithError(GlobalErrors.StatusNotInEnum);
    }
}
