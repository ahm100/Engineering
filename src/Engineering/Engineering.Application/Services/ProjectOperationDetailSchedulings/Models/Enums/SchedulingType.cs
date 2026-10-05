using System.ComponentModel;

namespace Engineering.Application.Services.ProjectOperationDetailSchedulings.Models.Enums;

public enum SchedulingType
{
    [Description("زمان بندی بر اساس انتخاب ریز متره")]
    ByProjectOperationDetail = 1,
    [Description("زمان بندی بر اساس شرح عملیات")]
    ByProjectOperation = 2,
}
