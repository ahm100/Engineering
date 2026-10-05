namespace Engineering.Domain.Entities.ProcesVerbal.Enums;

public enum ProcesVerbalProductItemStatus
{
    [Description("کامل")]
    Full = 1,

    [Description("ناقص")]
    Parted = 2,

    [Description("درحال انجام")]
    Processing = 3
}
