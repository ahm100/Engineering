namespace Engineering.Domain.Entities.ProcesVerbal.Enums;

public enum ProcesVerbalDeliveryStatus
{
    /// <summary>
    /// تحویل کامل
    /// </summary>
    [Description("تحویل کامل")]
    FullDelivery = 1,

    /// <summary>
    /// تویل جزیی
    /// </summary>
    [Description("تویل جزیی")]
    PartedDelivery = 2,

    /// <summary>
    /// تحویل نشد
    /// </summary>
    [Description("تحویل نشد")]
    NotDeliverd = 3
}
