using System.ComponentModel;

namespace Engineering.Application.Services.Transportations.Models.GetsTransportationExcelEnum;

public enum TransportationExcelEnum
{
    [Description("ردیف")]
    Row = 0,

    [Description("نام ترابری")]
    TransportationName = 1,

    [Description("کد ترابری")]
    TransportationCode = 2,

    [Description("مسافری")]
    IsPassenger = 3,

    [Description("وضعیت")]
    IsActive = 4,

    [Description("نام موسسه")]
    CompanyNameFa = 5
}
