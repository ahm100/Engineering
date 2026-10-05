namespace Engineering.Domain.Entities.FixAssetMachineries.Enums;

public enum MachineryReservationUnit
{
    /// <summary>
    /// روزانه
    /// </summary>
    [Description("روزانه")]
    Daily = 1,
    /// <summary>
    /// ساعتی
    /// </summary>
    [Description("ساعتی")]
    Hourly = 2,
    /// <summary>
    /// سرویسی
    /// </summary>
    [Description("سرویسی")]
    Serviced = 3,
    /// <summary>
    /// سرویسی
    /// </summary>
    [Description("مترمکعبی")]
    Volume = 4
}
