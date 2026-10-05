namespace Engineering.Application.Services.TelegramChats.Models.ExitRelocationForTemporaryDeliveryTelegramMessage;

public class ExitRelocationForTemporaryDeliveryTelegramMessageValidator : AbstractValidator<ExitRelocationForTemporaryDeliveryTelegramMessageRequest>
{
    public ExitRelocationForTemporaryDeliveryTelegramMessageValidator()
    {
        RuleFor(oo => oo.WarehouseId).NotEmpty();
    }
}