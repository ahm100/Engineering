namespace Engineering.Domain.Entities.WorkflowRequests.Enums;

public enum WorkflowExecutionStatus
{
    [Description("نامشخص")]
    Unknown = 0,

    [Description("در حال اجرا")]
    Running = 1,

    [Description("تکمیل شده")]
    Completed = 2,

    [Description("لغو شده")]
    Cancelled = 3,

    [Description("دارای خطا")]
    Faulted = 4
}