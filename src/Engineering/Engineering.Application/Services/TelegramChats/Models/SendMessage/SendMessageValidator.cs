namespace Engineering.Application.Services.TelegramChats.Models.SendMessage;

public class SendMessageValidator : AbstractValidator<SendMessageRequest>
{
    public SendMessageValidator()
    {
        RuleFor(c => c.TargetId)
            .IsPositive(GlobalCmts.Id);

    }
}