namespace Engineering.Application.Services.TelegramChats.Queries.GetTelegramChatById;

public class GetTelegramChatByIdQueryValidator : AbstractValidator<GetTelegramChatByIdQuery>
{
    public GetTelegramChatByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(TelegramChatErrors.IdIsEmpty);
    }
}