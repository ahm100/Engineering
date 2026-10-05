namespace Engineering.Application.Services.TelegramChats.Commands.StateChangerTelegramChats;

public class StateChangerTelegramChatsCommandValidator : AbstractValidator<StateChangerTelegramChatsCommand>
{
    public StateChangerTelegramChatsCommandValidator()
    {
        RuleFor(oo => oo.Items).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
