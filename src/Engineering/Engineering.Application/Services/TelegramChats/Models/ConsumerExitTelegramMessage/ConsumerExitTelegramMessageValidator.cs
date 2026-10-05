namespace Engineering.Application.Services.TelegramChats.Models.ConsumerExitTelegramMessage;

public class ConsumerExitTelegramMessageValidator : AbstractValidator<ConsumerExitTelegramMessageRequest>
{
    public ConsumerExitTelegramMessageValidator()
    {
        RuleFor(oo => oo.WarehouseId).NotEmpty();
    }
}