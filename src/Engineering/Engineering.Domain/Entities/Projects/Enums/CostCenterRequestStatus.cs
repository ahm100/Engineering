namespace Engineering.Domain.Entities.Projects.Enums;

public enum ProjectCostCenterRequestStatus
{
    [Description("در حال انجام")]
    InProgress = 1,

    [Description("درخواست رد شده")]
    Rejected = 2,

    [Description("انجام شده")]
    Completed = 3
}