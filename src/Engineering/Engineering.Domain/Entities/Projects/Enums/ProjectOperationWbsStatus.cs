namespace Engineering.Domain.Entities.Projects.Enums;

public enum ProjectOperationWbsStatus
{
    [Description("شروع نشده")]
    NotStarted = 1,
    [Description("دارای تاخیر")]
    Delayed = 2,
    [Description("در معرض تاخیر")]
    Critical = 3,
    [Description("طبق برنامه")]
    Safe = 4
}
