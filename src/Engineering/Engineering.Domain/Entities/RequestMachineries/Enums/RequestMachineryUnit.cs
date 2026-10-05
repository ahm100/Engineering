namespace Engineering.Domain.Entities.RequestMachineries.Enums;

public enum RequestMachineryUnit
{
    [Description("روزانه")]
    Daily = 1,
    [Description("ساعتی")]
    Hourly = 2,
    [Description("سرویسی")]
    Serviced = 3,
    [Description("مترمکعب")]
    Volume = 4
}
