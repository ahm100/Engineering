namespace Engineering.Application.Services.TelegramMessageHistorys.Commands.Delete;

public class DeleteTelegramMessageHistoryCommandValidator : AbstractValidator<DeleteTelegramMessageHistoryCommand>
{
    public DeleteTelegramMessageHistoryCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TelegramMessageHistoryErrors.UnValidId);
    }
}