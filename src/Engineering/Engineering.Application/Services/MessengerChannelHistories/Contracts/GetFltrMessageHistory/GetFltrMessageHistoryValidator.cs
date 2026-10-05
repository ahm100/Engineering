namespace Engineering.Application.Services.MessengerChannelHistories.Contracts.GetMessengerChannelHistories;

public class GetMessengerChannelHistoriesValidator : AbstractValidator<GetMessengerChannelHistoriesRequest>
{
    public GetMessengerChannelHistoriesValidator()
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
