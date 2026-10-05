namespace Engineering.Application.Services.Seasons.Models.GetSeasons;

public class GetSeasonsValidator : AbstractValidator<GetSeasonsRequest>
{
    public GetSeasonsValidator()
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
