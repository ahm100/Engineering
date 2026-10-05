namespace Engineering.Application.Services.TelegramChats.Models.EntryThroughRelocationForTemporaryDeliveryTelegramMessage;

public class EntryThroughRelocationForTemporaryDeliveryTelegramMessageValidator : AbstractValidator<EntryThroughRelocationForTemporaryDeliveryTelegramMessageRequest>
{
    public EntryThroughRelocationForTemporaryDeliveryTelegramMessageValidator()
    {
        RuleFor(oo => oo.WarehouseId).NotEmpty();
    }
}