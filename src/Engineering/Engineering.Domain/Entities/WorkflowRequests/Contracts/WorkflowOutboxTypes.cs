namespace Engineering.Domain.Entities.WorkflowRequests.Contracts;

/// <summary>
/// انواع پیام های پشتیبانی شده در ارسال به سرویس گردش کار.
/// </summary>
public static class WorkflowOutboxTypes
{
    public const string StartWorkflowV1 = "workflow.start.v1";
}
