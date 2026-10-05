
namespace Engineering.Application.Services.OperationInfoGroups.Models.OperationInfoGroupGroupDelete;

public class OperationInfoGroupGroupDeleteValidator : AbstractValidator<OperationInfoGroupGroupDeleteRequest>
{
    public OperationInfoGroupGroupDeleteValidator()
    {
        RuleFor(c => c.Ids).NotEmpty().WithError(GlobalErrors.IdsIsEmpty).NotNull().WithError(GlobalErrors.IdsIsNull);
        RuleForEach(c => c.Ids).GreaterThan(0).WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
