
namespace Engineering.Application.Services.PublicGroups.Models.PublicGroupGroupDelete;

public class PublicGroupGroupDeleteValidator : AbstractValidator<PublicGroupGroupDeleteRequest>
{
    public PublicGroupGroupDeleteValidator()
    {
        RuleFor(c => c.Ids).NotEmpty().WithError(GlobalErrors.IdsIsEmpty).NotNull().WithError(GlobalErrors.IdsIsNull);
        RuleForEach(c => c.Ids).GreaterThan(0).WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
