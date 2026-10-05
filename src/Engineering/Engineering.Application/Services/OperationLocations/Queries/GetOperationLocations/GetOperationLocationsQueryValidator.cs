namespace Engineering.Application.Services.OperationLocations.Queries.GetOperationLocations;

public class GetOperationLocationsQueryValidator : AbstractValidator<GetOperationLocationsQuery>
{
    public GetOperationLocationsQueryValidator()
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