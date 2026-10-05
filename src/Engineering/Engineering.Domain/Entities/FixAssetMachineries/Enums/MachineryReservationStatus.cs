namespace Engineering.Domain.Entities.FixAssetMachineries.Enums;

public enum MachineryReservationStatus
{
    /// <summary>
    /// شروع نشده
    /// </summary>
    [Description("شروع نشده")]
    NotsStarted = 1,

    /// <summary>
    /// درحال استفاده
    /// </summary>
    [Description("درحال استفاده")]
    InUse = 5,

    /// <summary>
    /// پایان یافته
    /// </summary>
    [Description("پایان یافته")]
    Finished = 10,

    /// <summary>
    /// کنسل شده
    /// </summary>
    [Description("کنسل شده")]
    Cancelled = 15,
}
