namespace Engineering.Domain.Entities.WorkflowRequests.Enums;

public enum WorkflowDispatchStatus
{
    [Description("در انتظار")]
    Pending = 0,

    [Description("تأیید شده")]
    Accepted = 1,

    [Description("شکست دائمی")]
    PermanentlyFailed = 2
}