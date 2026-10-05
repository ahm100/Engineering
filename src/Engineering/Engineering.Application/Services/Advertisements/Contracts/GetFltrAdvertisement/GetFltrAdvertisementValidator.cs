namespace Engineering.Application.Services.Advertisements.Contracts.GetFltrAdvertisement;

public class GetFltrAdvertisementValidator : AbstractValidator<GetFltrAdvertisementRequest>
{
    public GetFltrAdvertisementValidator()
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