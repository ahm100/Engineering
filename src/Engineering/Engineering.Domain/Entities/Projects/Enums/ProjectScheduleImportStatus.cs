namespace Engineering.Domain.Entities.Projects.Enums;

public enum ProjectScheduleImportStatus
{
    [Description("در حال انجام")]
    Processing = 1,

    [Description("انجام شده")]
    Completed = 2,

    [Description("دارای خطا")]
    Failed = 3,

    [Description("بایگانی شده")]
    Archived = 4
}