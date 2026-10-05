namespace Engineering.Application.Services.TelegramChats.Models.ExitForRelocationTelegramMessage;

public class ExitForRelocationTelegramMessageValidator : AbstractValidator<ExitForRelocationTelegramMessageRequest>
{
    public ExitForRelocationTelegramMessageValidator()
    {
        RuleFor(oo => oo.WarehouseId).NotEmpty();
    }
}