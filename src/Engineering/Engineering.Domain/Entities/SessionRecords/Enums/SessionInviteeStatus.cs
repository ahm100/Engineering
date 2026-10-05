namespace Engineering.Domain.Entities.SessionRecords.Enums;

public enum SessionInviteeStatus
{
    [Description("حاضر")]
    Present = 1,

    [Description("غایب")]
    Absent = 2,

    [Description("نماینده")]
    Deputy = 3
}
