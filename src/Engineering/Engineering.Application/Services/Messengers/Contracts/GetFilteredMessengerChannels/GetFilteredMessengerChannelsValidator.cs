using Engineering.Application.Services.GetFltrMessengerChannels.Contracts.GetFltrMessengerChannel;

namespace Engineering.Application.Services.Messengers.Contracts.GetMessengers;

public class GetMessengerChannelsValidator : AbstractValidator<GetMessengerChannelsRequest>
{
    public GetMessengerChannelsValidator()
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
