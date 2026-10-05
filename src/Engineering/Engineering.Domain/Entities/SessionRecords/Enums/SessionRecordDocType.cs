namespace Engineering.Domain.Entities.SessionRecords.Enums;

public enum SessionRecordDocType
{
    [Description("فایل ارائه جلسه")]
    Presentation = 1,

    [Description("گزارش")]
    Report = 2,

    [Description("نقشه")]
    Map = 3,

    [Description("نامه")]
    Letter = 4,

    [Description("مستندات مرتبط")]
    RelatedDocuments = 5,
}