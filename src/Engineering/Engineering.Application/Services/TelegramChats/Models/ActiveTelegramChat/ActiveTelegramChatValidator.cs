namespace Engineering.Application.Services.TelegramChats.Models.ActiveTelegramChat;

public class ActiveTelegramChatValidator : AbstractValidator<ActiveTelegramChatRequest>
{
    public ActiveTelegramChatValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TelegramChatErrors.IdIsEmpty);
    }
}
