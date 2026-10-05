namespace Engineering.Application.Services.TelegramChats.Models.SendTestTelegramMessage;

public class SendTestTelegramMessageValidator : AbstractValidator<SendTestTelegramMessageRequest>
{
    public SendTestTelegramMessageValidator()
    {
        RuleFor(oo => oo.ChatId).NotEmpty().WithError(TelegramChatErrors.InValidChatId);
    }
}