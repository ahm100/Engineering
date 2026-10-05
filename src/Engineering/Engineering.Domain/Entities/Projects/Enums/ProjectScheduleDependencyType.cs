namespace Engineering.Domain.Entities.Projects.Enums;

public enum ProjectScheduleDependencyType
{
    [Description("پایان به شروع")]
    FinishToStart = 1,

    [Description("شروع به شروع")]
    StartToStart = 2,

    [Description("پایان به پایان")]
    FinishToFinish = 3,

    [Description("شروع به پایان")]
    StartToFinish = 4
}