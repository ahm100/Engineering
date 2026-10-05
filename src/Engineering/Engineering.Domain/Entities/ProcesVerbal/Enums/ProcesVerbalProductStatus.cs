namespace Engineering.Domain.Entities.ProcesVerbal.Enums;

public enum ProcesVerbalProductStatus
{
    /// <summary>
    /// کامل
    /// </summary>
    [Description("کامل")]
    Full = 1,

    /// <summary>
    /// ناقص
    /// </summary>
    [Description("ناقص")]
    Parted = 2,

    /// <summary>
    /// درحال انجام
    /// </summary>
    [Description("درحال انجام")]
    Processing = 3
}
