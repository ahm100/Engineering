namespace Engineering.Application.Services.TelegramChats.Commands.InactiveTelegramChat;

public class InactiveTelegramChatCommandValidator : AbstractValidator<InactiveTelegramChatCommand>
{
    public InactiveTelegramChatCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TelegramChatErrors.IdIsEmpty);
    }
}