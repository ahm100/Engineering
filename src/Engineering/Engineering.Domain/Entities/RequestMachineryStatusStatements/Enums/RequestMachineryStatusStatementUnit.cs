namespace Engineering.Domain.Entities.RequestMachineryStatusStatements.Enums;

public enum RequestMachineryStatusStatementUnit
{
    [Description("روزانه")]
    Daily = 1,
    [Description("ساعتی")]
    Hourly = 2,
    [Description("سرویسی")]
    Serviced = 3,
    [Description("مترمکعبی")]
    Volume = 4
}
