namespace Engineering.Domain.Entities.ProjectOperations.Enums;

public enum ProjectOperationDependencyType
{
    [Description("پایان به شروع")]
    FS = 1,
    [Description("شروع به شروع")]
    SS = 2,
    [Description("پایان به پایان")]
    FF = 3,
    [Description("شروع به پایان")]
    SF = 4
}