using System.ComponentModel;

namespace Engineering.Application.Services.BillOfLadings.Contracts.GetsBillOfLadingExcelEnum;

public record GetsBillOfLadingExcelEnumResponse(List<EnumObject> Data);

public enum BillOfLadingExcelEnum
{
    [DefaultHeader]
    [Description("ردیف")]
    Row = 0,

    [DefaultHeader]
    [Description("نام بارنامه")]
    BillOfLadingName = 1,

    [DefaultHeader]
    [Description("کد بارنامه")]
    BillOfLadingCode = 2,

    [DefaultHeader]
    [Description("وضعیت")]
    IsActive = 3,

}