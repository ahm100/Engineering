using System.ComponentModel;

namespace Engineering.Api.Controllers.ProjectOperations.Contracts.GetsFilteredByProjectIds;

public enum FltrProjectOperationEnum
{
    [Description("شناسه")]
    Id = 1,

    [Description("شناسه اطلاعات عملیات")]
    OperationInfoId,

    [Description("نام اطلاعات عملیات")]
    OperationInfoName,

    [Description("کد اطلاعات عملیات")]
    OperationInfoCode,

    [Description("شناسه واحد اطلاعات عملیات")]
    OperationInfoMeasurementId,

    [Description("نام واحد اطلاعات عملیات")]
    OperationInfoMeasurementName,

    [Description("شناسه واحد")]
    MeasurementId,

    [Description("نام واحد")]
    MeasurementName,

    [Description("حجم کار")]
    Workload
}