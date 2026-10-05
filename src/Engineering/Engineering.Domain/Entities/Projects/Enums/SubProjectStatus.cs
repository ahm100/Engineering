namespace Engineering.Domain.Entities.Projects.Enums;

public enum SubProjectStatus
{
    [Description("پیش نویس")]
    Draft = 1,
    [Description("فعال")]
    Active = 2,
    [Description("تکمیل شده")]
    Completed = 3
}
