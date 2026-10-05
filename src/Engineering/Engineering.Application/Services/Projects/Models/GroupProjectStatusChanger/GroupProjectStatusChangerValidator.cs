
namespace Engineering.Application.Services.Projects.Models.GroupProjectStatusChanger;

public class GroupProjectStatusChangerValidator : AbstractValidator<GroupProjectStatusChangerRequest>
{
    public GroupProjectStatusChangerValidator()
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
