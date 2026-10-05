namespace Engineering.Application.Services.TelegramChats.Commands.ActiveTelegramChat;

public class ActiveTelegramChatCommandValidator : AbstractValidator<ActiveTelegramChatCommand>
{
    public ActiveTelegramChatCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TelegramChatErrors.IdIsEmpty);
    }
}