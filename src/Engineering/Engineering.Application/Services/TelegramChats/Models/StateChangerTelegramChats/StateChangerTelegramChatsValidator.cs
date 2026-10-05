
namespace Engineering.Application.Services.TelegramChats.Models.StateChangerTelegramChats;

public class StateChangerTelegramChatsValidator : AbstractValidator<StateChangerTelegramChatsRequest>
{
    public StateChangerTelegramChatsValidator()
    {
        RuleFor(oo => oo.Ids).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
