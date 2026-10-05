namespace Engineering.Application.Services.TelegramChats.Queries.GetsTelegramChatByCostCenterIds;

public class GetsTelegramChatByCostCenterIdsQueryValidator : AbstractValidator<GetsTelegramChatByCostCenterIdsQuery>
{
    public GetsTelegramChatByCostCenterIdsQueryValidator()
    {
        RuleFor(oo => oo.PageIndex).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageIndexNotValid)
            .LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexNotValid);
        RuleFor(oo => oo.PageSize).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageSizeNotValid)
            .LessThanOrEqualTo(GlobalErrors.MaxSize).WithError(GlobalErrors.PageSizeNotValid);
    }
}