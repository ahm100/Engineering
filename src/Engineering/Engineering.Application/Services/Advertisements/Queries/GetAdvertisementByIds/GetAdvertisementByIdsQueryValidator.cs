namespace Engineering.Application.Services.Advertisements.Queries.GetAdvertisementByIds;

public class GetAdvertisementByIdsQueryValidator : AbstractValidator<GetAdvertisementByIdsQuery>
{
    public GetAdvertisementByIdsQueryValidator()
    {
        RuleForEach(c => c.Ids)
            .IsPositive(GlobalCmts.Id);
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