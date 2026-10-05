namespace Engineering.Application.Services.TelegramChats.Models.GetByCostCenterId;

public class GetByCostCenterIdValidator : AbstractValidator<GetByCostCenterIdRequest>
{
    public GetByCostCenterIdValidator()
    {
        RuleFor(oo => oo.CostCenterId).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TelegramChatErrors.CostCenterIdIsEmpty);
        RuleFor(oo => oo.PageIndex).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageIndexNotValid)
            .LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexNotValid);
        RuleFor(oo => oo.PageSize).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageSizeNotValid)
            .LessThanOrEqualTo(GlobalErrors.MaxSize).WithError(GlobalErrors.PageSizeNotValid);
    }
}