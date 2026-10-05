
namespace Engineering.Application.Services.OperationInfos.Models.OperationInfoGroupDelete;

public class OperationInfoGroupDeleteValidator : AbstractValidator<OperationInfoGroupDeleteRequest>
{
    public OperationInfoGroupDeleteValidator()
    {
        RuleFor(c => c.Ids).NotEmpty().WithError(GlobalErrors.IdsIsEmpty).NotNull().WithError(GlobalErrors.IdsIsNull);
        RuleForEach(c => c.Ids).GreaterThan(0).WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
