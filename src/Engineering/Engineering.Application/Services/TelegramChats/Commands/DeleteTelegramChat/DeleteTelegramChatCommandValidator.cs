namespace Engineering.Application.Services.TelegramChats.Commands.DeleteTelegramChat;

public class DeleteTelegramChatCommandValidator : AbstractValidator<DeleteTelegramChatCommand>
{
    public DeleteTelegramChatCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
                                                  .WithError(TelegramChatErrors.IdIsEmpty);
    }
}