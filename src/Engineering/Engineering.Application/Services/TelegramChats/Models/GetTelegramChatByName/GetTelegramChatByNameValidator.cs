namespace Engineering.Application.Services.TelegramChats.Models.GetTelegramChatByName;

public class GetTelegramChatByNameValidator : AbstractValidator<GetTelegramChatByNameRequest>
{
    public GetTelegramChatByNameValidator()
    {
        RuleFor(oo => oo.ChatName).NotEmpty().WithError(TelegramChatErrors.TelegramChatNameIsEmpty);
    }
}
