namespace Engineering.Application.Services.TelegramChats.Models.CreateTelegramChat;

public class CreateTelegramChatValidator : AbstractValidator<CreateTelegramChatRequest>
{
    public CreateTelegramChatValidator()
    {
        RuleFor(oo => oo.CostCenterId).NotNull().GreaterThanOrEqualTo(1).NotEmpty()
            .WithError(TelegramChatErrors.CostCenterIdIsEmpty);
        RuleFor(oo => oo.ChatId).NotEmpty().WithError(TelegramChatErrors.ChatIdIsEmpty);
        RuleFor(oo => oo.ChatName).NotEmpty().WithError(TelegramChatErrors.TelegramChatNameIsEmpty)
            .Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);
        RuleFor(oo => oo.ChatUrl).NotEmpty().WithError(TelegramChatErrors.TelegramChatUrlIsEmpty);
        RuleFor(oo => oo.IsActive).NotNull().WithError(TelegramChatErrors.IsActiveIsEmpty);
    }
}