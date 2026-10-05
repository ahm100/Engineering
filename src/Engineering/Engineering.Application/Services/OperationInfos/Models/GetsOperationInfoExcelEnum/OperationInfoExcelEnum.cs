using System.ComponentModel;

namespace Engineering.Application.Services.OperationInfos.Models.GetsOperationInfoExcelEnum;

public enum OperationInfoExcelEnum
{
    [Description("ردیف")]
    Row = 0,

    [Description("نام شرح عملیات")]
    OperationInfoName = 1,

    [Description("کد شرح عملیات")]
    OperationInfoCode = 2,

    [Description("نام لاتین شرح عملیات")]
    OperationLatinName = 3,

    [Description("اولویت")]
    Priority = 4,

    [Description("نام واحد")]
    MeasurementName = 5,

    [Description("نام وابستگی")]
    DependencyName = 6,

    [Description("کد وابستگی")]
    DependencyCode = 7,

    [Description("اولویت وابستگی")]
    DependencyPriority = 8,

    [Description("روزهای کاری")]
    WorkingDays = 9,

    [Description("نوع وابستگی")]
    DependencyType = 10,

    [Description("رسته")]
    Categories = 11,

    [Description("رشته")]
    Branchs = 12,

    [Description("فصل")]
    Seasons = 13,

    [Description("وضعیت")]
    IsActive = 14,

    [Description("گروه")]
    Groups = 15,

    [Description("نام موسسه")]
    CompanyNameFa = 16
}