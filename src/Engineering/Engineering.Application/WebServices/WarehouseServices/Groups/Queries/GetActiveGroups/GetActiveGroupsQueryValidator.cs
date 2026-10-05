namespace Engineering.Application.WebServices.WarehouseServices.Groups.Queries.GetActiveGroups;

public class GetActiveGroupsQueryValidator : AbstractValidator<GetActiveGroupsQuery>
{
    public GetActiveGroupsQueryValidator()
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