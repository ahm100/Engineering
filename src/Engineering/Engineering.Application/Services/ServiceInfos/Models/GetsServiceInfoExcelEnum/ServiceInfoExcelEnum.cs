using System.ComponentModel;

namespace Engineering.Application.Services.ServiceInfos.Models.GetsServiceInfoExcelEnum;

public enum ServiceInfoExcelEnum
{
    [DefaultHeader]
    [Description("ردیف")]
    Row = 0,

    [DefaultHeader]
    [Description("نام خدمت")]
    ServiceInfoName = 1,

    [DefaultHeader]
    [Description("کد خدمت")]
    ServiceInfoCode = 2,

    [DefaultHeader]
    [Description("واحد اندازه گیری")]
    MeasurementName = 3,

    [DefaultHeader]
    [Description("وضعیت")]
    IsActive = 4
}
