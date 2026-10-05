namespace Engineering.Application.Services.TelegramMessageHistorys.Commands.SetIsSend;

public class SetIsSendTelegramMessageHistoryCommandValidator : AbstractValidator<SetIsSendTelegramMessageHistoryCommand>
{
    public SetIsSendTelegramMessageHistoryCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TelegramMessageHistoryErrors.UnValidId);
    }
}