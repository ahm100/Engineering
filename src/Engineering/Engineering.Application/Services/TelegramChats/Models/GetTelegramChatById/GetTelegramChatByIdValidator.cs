namespace Engineering.Application.Services.TelegramChats.Models.GetTelegramChatById;

public class GetTelegramChatByIdValidator : AbstractValidator<GetTelegramChatByIdRequest>
{
    public GetTelegramChatByIdValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TelegramChatErrors.IdIsEmpty);
    }
}
