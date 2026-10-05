
namespace Engineering.Application.Services.OperationLocations.Models.OperationLocationGroupDelete;

public class OperationLocationGroupDeleteValidator : AbstractValidator<OperationLocationGroupDeleteRequest>
{
    public OperationLocationGroupDeleteValidator()
    {
        RuleFor(c => c.Ids).NotEmpty().WithError(GlobalErrors.IdsIsEmpty).NotNull().WithError(GlobalErrors.IdsIsNull);
        RuleForEach(c => c.Ids).GreaterThan(0).WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
