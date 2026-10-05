namespace Engineering.Application.Services.TelegramMessageHistorys.Models.ExecuteSendTelegramMessage;

public class ExecuteSendTelegramMessageValidator : AbstractValidator<ExecuteSendTelegramMessageRequest>
{
    public ExecuteSendTelegramMessageValidator()
    {
        RuleFor(oo => oo.Ids).NotEmpty().WithError(TelegramMessageHistoryErrors.UnValidId);
    }
}