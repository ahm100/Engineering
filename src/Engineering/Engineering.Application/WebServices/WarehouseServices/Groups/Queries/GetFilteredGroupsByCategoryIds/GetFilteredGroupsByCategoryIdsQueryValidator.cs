namespace Engineering.Application.WebServices.WarehouseServices.Groups.Queries.GetFilteredGroupsByCategoryIds;

public class GetFilteredGroupsByCategoryIdsQueryValidator : AbstractValidator<GetFilteredGroupsByCategoryIdsQuery>
{
    public GetFilteredGroupsByCategoryIdsQueryValidator()
    {
        RuleFor(oo => oo.CategoryIds).NotEmpty().WithError(MetaDataErrors.IdIsEmpty);

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