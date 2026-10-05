namespace Engineering.Application.Services.TelegramChats.Models.GetsTelegramChatByCostCenterIds;

public class GetsTelegramChatByCostCenterIdsRequestValidator : AbstractValidator<GetsTelegramChatByCostCenterIdsRequest>
{
    public GetsTelegramChatByCostCenterIdsRequestValidator()
    {
        RuleFor(oo => oo.CostCenterIds).NotNull().WithError(TelegramChatErrors.CostCenterIdIsEmpty);
        RuleFor(oo => oo.PageIndex).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageIndexNotValid)
            .LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexNotValid);
        RuleFor(oo => oo.PageSize).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageSizeNotValid)
            .LessThanOrEqualTo(GlobalErrors.MaxSize).WithError(GlobalErrors.PageSizeNotValid);
    }
}