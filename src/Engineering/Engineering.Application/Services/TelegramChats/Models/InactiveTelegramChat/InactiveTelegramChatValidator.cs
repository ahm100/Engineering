namespace Engineering.Application.Services.TelegramChats.Models.InactiveTelegramChat;

public class InactiveTelegramChatValidator : AbstractValidator<InactiveTelegramChatRequest>
{
    public InactiveTelegramChatValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TelegramChatErrors.IdIsEmpty);
    }
}
