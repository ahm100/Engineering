
namespace Engineering.Application.Services.TelegramChats.Models.TelegramChatGroupDelete;

public class TelegramChatGroupDeleteValidator : AbstractValidator<TelegramChatGroupDeleteRequest>
{
    public TelegramChatGroupDeleteValidator()
    {
        RuleFor(c => c.Ids).NotEmpty().WithError(GlobalErrors.IdsIsEmpty).NotNull().WithError(GlobalErrors.IdsIsNull);
        RuleForEach(c => c.Ids).GreaterThan(0).WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
