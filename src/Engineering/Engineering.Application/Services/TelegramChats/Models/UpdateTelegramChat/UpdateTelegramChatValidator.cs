namespace Engineering.Application.Services.TelegramChats.Models.UpdateTelegramChat;

public class UpdateTelegramChatValidator : AbstractValidator<UpdateTelegramChatRequest>
{
    public UpdateTelegramChatValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TelegramChatErrors.IdIsEmpty);
        RuleFor(oo => oo.CostCenterId).NotNull().GreaterThanOrEqualTo(1).NotEmpty()
            .WithError(TelegramChatErrors.CostCenterIdIsEmpty);
        RuleFor(oo => oo.ChatId).NotEmpty().WithError(TelegramChatErrors.ChatIdIsEmpty);
        RuleFor(oo => oo.ChatName).NotEmpty().WithError(TelegramChatErrors.TelegramChatNameIsEmpty)
            .Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);
        RuleFor(oo => oo.ChatUrl).NotEmpty().WithError(TelegramChatErrors.TelegramChatUrlIsEmpty);
        RuleFor(oo => oo.IsActive).NotNull().WithError(TelegramChatErrors.IsActiveIsEmpty);
    }
}