using System.ComponentModel;

namespace Engineering.Application.Services.EmployerStatusStatements.Models.GetsEmployerStatusStatementExcelEnums;

public enum EmployerStatusStatementExcelEnum
{
    [Description("ردیف")]
    Row = 0,

    [Description("شناسه")]
    Id = 1,

    [Description("وضعیت")]
    SendStatusTypeTitle = 2,

    [Description("تاریخ شروع")]
    StartDate = 3,

    [Description("تاریخ پایان")]
    EndDate = 4,

    [Description("کد صورت وضعیت")]
    StatusStatementCode = 5,

    [Description("نام کارفرما")]
    EmployerName = 6,

    [Description("شماره قرارداد کافرما")]
    EmployerContractCode = 7,

    [Description("نام مرکزهزینه")]
    CostCenterName = 8,

    [Description("نام پروژه")]
    ProjectName = 9,

    [Description("درست انجام شده")]
    PercentageOfWorkDone = 10,

    [Description("مبلغ محاسیه شده")]
    CalculatedAmount = 11,

    [Description("نام ارز")]
    CurrencyName = 12,

    [Description("تاریخ ایجاد")]
    Created = 13,

    [Description("شناسه موسسه")]
    CompanyId = 14,

    [Description("نام موسسه")]
    CompanyNameFa = 15
}