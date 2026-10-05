using System.ComponentModel;

namespace Engineering.Application.Services.EmployerStatusStatements.Models.GetsEmployerStatusStatementProjectOperationExcelEnum;

public enum EmployerStatusStatementProjectOperationExcelEnum
{
    [Description("ردیف")]
    Row = 0,

    [Description("شناسه")]
    Id = 1,

    [Description("شناسه عملیات")]
    ProjectOperationId = 2,

    [Description("وضعیت عملیات")]
    ProjectOperationStatusTitle = 3,

    [Description("نام شرح عملیات")]
    OperationInfoName = 4,

    [Description("حجم کل کار")]
    TotalWorkVolume = 5,

    [Description("حجم کار انجام شده")]
    DoneWorkVolume = 6,

    [Description("حجم کار صورت وضعیت")]
    StatusStatementWorkVolume = 7,

    [Description("واحد اندازگیری")]
    UnitOfMeasurementName = 8,

    [Description("درصد انجام کار")]
    DonePercentage = 9,

    [Description("کل درصد")]
    TotalPercentage = 10,

    [Description("مبلغ محاسبه شده")]
    CalculatedAmount = 11
}
