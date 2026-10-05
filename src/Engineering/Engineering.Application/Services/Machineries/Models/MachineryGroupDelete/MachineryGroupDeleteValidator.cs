
namespace Engineering.Application.Services.Machineries.Models.MachineryGroupDelete;

public class MachineryGroupDeleteValidator : AbstractValidator<MachineryGroupDeleteRequest>
{
    public MachineryGroupDeleteValidator()
    {
        RuleFor(c => c.Ids).NotEmpty().WithError(GlobalErrors.IdsIsEmpty).NotNull().WithError(GlobalErrors.IdsIsNull);
        RuleForEach(c => c.Ids).GreaterThan(0).WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
