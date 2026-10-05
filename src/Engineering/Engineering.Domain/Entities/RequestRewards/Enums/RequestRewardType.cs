namespace Engineering.Domain.Entities.RequestRewards.Enums;


public enum RequestRewardType
{
    [Description("پاداش")]
    Reward = 1,

    [Description("جریمه")]
    Fine = 2,

    [Description("حسن انجام کار")]
    GoodPerformance = 3,

    [Description("تخفیف")]
    Discount = 4,

    [Description("هدیه")]
    Gift = 5,

    [Description("کسر کار")]
    WorkDeduction = 6,

    [Description("جریمه کالای امانی")]
    FiduciaryProductFine = 8,
}

public class RRewardTypeRules
{
    public static List<RequestRewardType> AllowReward =
        [
        RequestRewardType.Reward,
        RequestRewardType.GoodPerformance,
        RequestRewardType.Gift,
    ];

    public static List<RequestRewardType> AllowFine =
        [
        RequestRewardType.Fine,
        RequestRewardType.Discount,
        RequestRewardType.WorkDeduction,
        RequestRewardType.FiduciaryProductFine,
    ];
}