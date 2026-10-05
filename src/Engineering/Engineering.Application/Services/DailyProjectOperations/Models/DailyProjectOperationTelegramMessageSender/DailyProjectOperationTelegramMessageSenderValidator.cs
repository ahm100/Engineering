namespace Engineering.Application.Services.DailyProjectOperations.Models.DailyProjectOperationTelegramMessageSender;

public class DailyProjectOperationTelegramMessageSenderValidator : AbstractValidator<DailyProjectOperationTelegramMessageSenderRequest>
{
    public DailyProjectOperationTelegramMessageSenderValidator()
    {
        RuleFor(oo => oo.Id)
            .NotNull().WithError(DailyProjectOperationErrors.InValidDailyProjectOperationId);
    }
}
