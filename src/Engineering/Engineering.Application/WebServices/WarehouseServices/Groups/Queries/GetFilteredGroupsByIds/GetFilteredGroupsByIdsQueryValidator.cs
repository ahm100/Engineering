namespace Engineering.Application.WebServices.WarehouseServices.Groups.Queries.GetFilteredGroupsByIds;

public class GetFilteredGroupsByIdsQueryValidator : AbstractValidator<GetFilteredGroupsByIdsQuery>
{
    public GetFilteredGroupsByIdsQueryValidator()
    {
        RuleFor(oo => oo.Ids).NotEmpty().WithError(MetaDataErrors.IdIsEmpty);

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