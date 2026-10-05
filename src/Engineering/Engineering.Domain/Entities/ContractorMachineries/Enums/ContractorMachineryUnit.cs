namespace Engineering.Domain.Entities.ContractorMachineries.Enums;

public enum ContractorMachineryUnit
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
    /// مترمکعبی
    /// </summary>
    [Description("مترمکعبی")]
    Volume = 4
}
