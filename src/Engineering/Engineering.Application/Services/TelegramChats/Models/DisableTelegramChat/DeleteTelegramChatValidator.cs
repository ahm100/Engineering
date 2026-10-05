namespace Engineering.Application.Services.TelegramChats.Models.DeleteTelegramChat;

public class DeleteTelegramChatValidator : AbstractValidator<DeleteTelegramChatRequest>
{
    public DeleteTelegramChatValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TelegramChatErrors.IdIsEmpty);
    }
}
