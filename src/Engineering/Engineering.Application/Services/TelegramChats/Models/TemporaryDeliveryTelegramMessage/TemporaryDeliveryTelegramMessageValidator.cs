namespace Engineering.Application.Services.TelegramChats.Models.TemporaryDeliveryTelegramMessage;

public class TemporaryDeliveryTelegramMessageValidator : AbstractValidator<TemporaryDeliveryTelegramMessageRequest>
{
    public TemporaryDeliveryTelegramMessageValidator()
    {
        RuleFor(oo => oo.WarehouseId).NotEmpty();
    }
}