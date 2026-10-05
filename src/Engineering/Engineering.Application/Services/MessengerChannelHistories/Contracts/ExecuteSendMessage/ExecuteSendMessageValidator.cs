namespace Engineering.Application.Services.TelegramMessageHistorys.Models.ExecuteSendMessage;

public class ExecuteSendMessageValidator : AbstractValidator<ExecuteSendMessageRequest>
{
    public ExecuteSendMessageValidator()
    {
        RuleForEach(oo => oo.Ids).IsPositive(GlobalCmts.Id);
    }
}