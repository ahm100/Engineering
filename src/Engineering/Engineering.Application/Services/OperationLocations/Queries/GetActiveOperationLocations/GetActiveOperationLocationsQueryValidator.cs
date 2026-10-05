namespace Engineering.Application.Services.OperationLocations.Queries.GetActiveOperationLocations;

public class GetActiveOperationLocationsQueryValidator : AbstractValidator<GetActiveOperationLocationsQuery>
{
    public GetActiveOperationLocationsQueryValidator()
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