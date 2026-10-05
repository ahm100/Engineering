using System.ComponentModel;

namespace Engineering.Application.Services.Seasons.Models.GetsSeasonExcelEnum;

public enum SeasonExcelEnum
{
    [Description("ردیف")]
    Row = 0,

    [Description("شناسه")]
    Id = 1,

    [Description("نام فصل")]
    SeasonName = 2,

    [Description("کد فصل")]
    SeasonCode = 3,

    [Description("شناسه رشته")]
    BranchId = 4,

    [Description("نام رشته")]
    BranchName = 5,

    [Description("کد رشته")]
    BranchCode = 6,

    [Description("وضعیت")]
    IsActive = 7,

    [Description("شناسه موسسه")]
    CompanyId = 8,

    [Description("نام موسسه")]
    CompanyNameFa = 9
}