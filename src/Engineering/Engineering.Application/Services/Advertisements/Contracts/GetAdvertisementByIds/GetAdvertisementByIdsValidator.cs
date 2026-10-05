namespace Engineering.Application.Services.Advertisements.Contracts.GetAdvertisementByIds;

public class GetAdvertisementByIdsValidator : AbstractValidator<GetAdvertisementByIdsRequest>
{
    public GetAdvertisementByIdsValidator()
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