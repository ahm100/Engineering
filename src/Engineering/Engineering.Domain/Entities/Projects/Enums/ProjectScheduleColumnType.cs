namespace Engineering.Domain.Entities.Projects.Enums;

public enum ProjectScheduleColumnType
{
    [Description("شکست کار")]
    Wbs = 1,
    [Description("عنوان")]
    Title = 2,
    [Description("مدت زمان")]
    Duration = 3,
    [Description("شروع")]
    Start = 4,
    [Description("پایان")]
    Finish = 5,
    [Description("وابستگی")]
    Predecessor = 6,
    [Description("وزن")]
    Weight = 7,
    [Description("پیشرفت برنامه ای")]
    PlannedProgress = 8,
    [Description("پیشرفت واقعی")]
    ActualProgress = 9,
    [Description("سفارشی")]
    Custom = 10
}
