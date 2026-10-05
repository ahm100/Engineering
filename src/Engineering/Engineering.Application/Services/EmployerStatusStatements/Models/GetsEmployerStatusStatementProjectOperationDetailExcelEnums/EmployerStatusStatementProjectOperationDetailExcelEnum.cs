using System.ComponentModel;

namespace Engineering.Application.Services.EmployerStatusStatements.Models.GetsEmployerStatusStatementProjectOperationDetailExcelEnums;

public enum EmployerStatusStatementProjectOperationDetailExcelEnum
{
    [Description("ردیف")]
    Row = 0,

    [Description("شناسه")]
    Id = 1,

    [Description("شناسه عملیات")]
    ProjectOperationId = 2,

    [Description("شناسه ریزمتره")]
    ProjectOperationDetailId = 3,

    [Description("وضعیت ریزمتره")]
    ProjectOperationDetailStatusTitle = 4,

    [Description("نام موقعیت جزئی")]
    PrivateName = 5,

    [Description("کد موقعیت جزئی")]
    PrivateCode = 6,

    [Description("حجم کل کار")]
    TotalWorkVolume = 7,

    [Description("حجم کار انجام شده")]
    DoneWorkVolume = 8,

    [Description("حجم کار صورت وضعیت")]
    StatusStatementWorkVolume = 9,

    [Description("درصد انجام کار")]
    DonePercentage = 10,

    [Description("کل درصد")]
    TotalPercentage = 11,

    [Description("مبلغ محاسبه شده")]
    CalculatedAmount = 12
}