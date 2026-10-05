namespace Engineering.Domain.Entities.SessionRecords.Enums;

public enum SessionRecordActionStatus
{
    [Description("باز")]
    Open = 1,

    [Description("درحال انجام")]
    InProgress = 2,

    [Description("انجام شده")]
    Completed = 3,

    [Description("لغو شده")]
    Canceled = 4
}
