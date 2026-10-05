namespace Engineering.Domain.Entities.RequestContractors.Enums;

public enum RequestContractorType
{
    [Description("مقطوع")]
    Fixe = 1,
    [Description("شرح خدمتی")]
    Service = 2,
    [Description("نفر حرفه‌ای روز")]
    Person = 3
}
