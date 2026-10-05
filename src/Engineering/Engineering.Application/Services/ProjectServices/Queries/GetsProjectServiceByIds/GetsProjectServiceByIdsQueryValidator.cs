
namespace Engineering.Application.Services.ProjectServices.Queries.GetsProjectServiceByIds;

public class GetsProjectServiceByIdsQueryValidator : AbstractValidator<GetsProjectServiceByIdsQuery>
{
    public GetsProjectServiceByIdsQueryValidator()
    {
        RuleFor(oo => oo.Items).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
