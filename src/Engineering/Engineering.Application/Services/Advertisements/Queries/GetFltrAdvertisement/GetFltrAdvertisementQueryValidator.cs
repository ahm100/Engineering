namespace Engineering.Application.Services.Advertisements.Queries.GetFltrAdvertisement;

public class GetFltrAdvertisementQueryValidator : AbstractValidator<GetFltrAdvertisementQuery>
{
    public GetFltrAdvertisementQueryValidator()
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