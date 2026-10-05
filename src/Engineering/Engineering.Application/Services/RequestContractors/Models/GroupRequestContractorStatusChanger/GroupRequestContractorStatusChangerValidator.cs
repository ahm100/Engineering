
namespace Engineering.Application.Services.RequestContractors.Models.GroupRequestContractorStatusChanger;

public class GroupRequestContractorStatusChangerValidator : AbstractValidator<GroupRequestContractorStatusChangerRequest>
{
    public GroupRequestContractorStatusChangerValidator()
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
