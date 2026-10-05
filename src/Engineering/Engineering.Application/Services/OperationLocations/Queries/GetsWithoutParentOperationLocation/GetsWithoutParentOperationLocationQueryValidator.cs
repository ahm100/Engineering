
namespace Engineering.Application.Services.OperationLocations.Queries.GetsWithoutParentOperationLocation;

public class GetsWithoutParentOperationLocationQueryValidator : AbstractValidator<GetsWithoutParentOperationLocationQuery>
{
    public GetsWithoutParentOperationLocationQueryValidator()
    {
        RuleFor(c => c.PageIndex)
            .PageIndexZero(GlobalCmts.PageIndex);
        RuleFor(c => c.PageSize)
            .PageSizeZero(GlobalCmts.PageSize);
        When(v => v.PageSize > 0, () =>
        {
            RuleFor(c => c.PageIndex)
                .PageIndexOne(GlobalCmts.PageIndex);
        });
    }
}
