namespace Engineering.Application.Services.TelegramMessageHistorys.Commands.Create;

public class CreateTelegramMessageHistoryCommandValidator : AbstractValidator<CreateTelegramMessageHistoryCommand>
{
    public CreateTelegramMessageHistoryCommandValidator()
    {
        RuleFor(oo => oo.TelegramChatId).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TelegramMessageHistoryErrors.UnValidTelegramChatId);

        RuleFor(oo => oo.TelegramMessageType).NotNull()
            .WithError(TelegramMessageHistoryErrors.UnValidTelegramChatId);

        RuleFor(oo => oo.Message).NotEmpty()
            .WithError(TelegramMessageHistoryErrors.UnValidMessage);
    }
}