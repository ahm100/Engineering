using System.ComponentModel;

namespace Engineering.Application.Services.MachineTypes.Models.GetsMachineTypeExcelEnum;

public enum MachineTypeExcelEnum
{
    [Description("ردیف")]
    Row = 0,

    [Description("نام نوع ماشین")]
    MachineTypeName = 1,

    [Description("کد نوع ماشین")]
    MachineTypeCode = 2,

    [Description("وزن از")]
    FromWeight = 3,

    [Description("وزن تا")]
    UntilWeight = 4,

    [Description("نام نوع اتاقک")]
    CabinTypeName = 5,

    [Description("کد نوع اتاقک")]
    CabinTypeCode = 6,

    [Description("وضعیت")]
    IsActive = 7,

    [Description("نام موسسه")]
    CompanyNameFa = 8,
}
