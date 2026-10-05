namespace Engineering.Domain.Entities.ProcesVerbal.Enums;

public enum ProcesVerbalWorkStatus
{
    [Description("آماده تحویل")]
    Final = 1,

    [Description("دارای نقص")]
    HaveLimits = 2,

    [Description("غیر آماده")]
    NotFinal = 3,
}
