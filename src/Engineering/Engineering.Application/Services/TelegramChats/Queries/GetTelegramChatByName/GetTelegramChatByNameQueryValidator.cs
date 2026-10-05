namespace Engineering.Application.Services.TelegramChats.Queries.GetTelegramChatByName;

public class GetTelegramChatByNameQueryValidator : AbstractValidator<GetTelegramChatByNameQuery>
{
    public GetTelegramChatByNameQueryValidator()
    {
        RuleFor(oo => oo.ChatName).NotEmpty().WithError(TelegramChatErrors.TelegramChatNameIsEmpty);
    }
}