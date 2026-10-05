
namespace Engineering.Application.Services.ProjectTypes.Queries.GetsProjectTypeByIds;

public class GetsProjectTypeByIdsQueryValidator : AbstractValidator<GetsProjectTypeByIdsQuery>
{
    public GetsProjectTypeByIdsQueryValidator()
    {
        RuleFor(oo => oo.Items).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
