namespace Engineering.Domain.Entities.Projects.Enums;

public enum SubProjectType
{
    [Description("مهندسی")]
    Engineering = 1,
    [Description("تدارکات")]
    Procurement = 2,
    [Description("ساخت")]
    Construction = 3,
    [Description("نصب و راه اندازی")]
    InstallationAndCommissioning = 4,
    [Description("پشتیبانی")]
    Support = 5
}
