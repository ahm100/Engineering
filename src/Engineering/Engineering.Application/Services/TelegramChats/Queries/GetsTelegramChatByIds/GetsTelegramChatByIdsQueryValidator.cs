
namespace Engineering.Application.Services.TelegramChats.Queries.GetsTelegramChatByIds;

public class GetsTelegramChatByIdsQueryValidator : AbstractValidator<GetsTelegramChatByIdsQuery>
{
    public GetsTelegramChatByIdsQueryValidator()
    {
        RuleFor(oo => oo.Ids).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
