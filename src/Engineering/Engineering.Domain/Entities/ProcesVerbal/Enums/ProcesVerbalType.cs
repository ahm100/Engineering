namespace Engineering.Domain.Entities.ProcesVerbal.Enums;

public enum ProcesVerbalType
{
    /// <summary>
    /// تحویل زمین
    /// </summary>
    [Description("تحویل زمین")]
    LandDelivery = 1,

    /// <summary>
    /// تحویل کارگاه
    /// </summary>
    [Description("تحویل کارگاه")]
    WorkshopDelivery = 2,

    /// <summary>
    /// تجهیز کارگاه
    /// </summary>
    [Description("تجهیز کارگاه")]
    WorkshopEquipping = 3,

    /// <summary>
    /// صورت برداری
    /// </summary>
    //[Description("صورت برداری")]
    //InventoryTaking = 4,

    /// <summary>
    /// تحویل موقت
    /// </summary>
    [Description("تحویل موقت")]
    TempDelivery = 5,

    /// <summary>
    /// تحویل قطعی
    /// </summary>
    [Description("تحویل قطعی")]
    FinalDelivery = 6,

    /// <summary>
    /// تغییر مقادیر
    /// </summary>
    [Description("تغییر مقادیر")]
    ChangingValues = 7,

    /// <summary>
    /// شروع به کار
    /// </summary>
    [Description("شروع به کار")]
    WorkStart = 8,

    /// <summary>
    /// توقف کار
    /// </summary>
    [Description("توقف کار")]
    WorkStop = 9
}
