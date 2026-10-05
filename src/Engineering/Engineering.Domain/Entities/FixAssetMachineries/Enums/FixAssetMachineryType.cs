namespace Engineering.Domain.Entities.FixAssetMachineries.Enums;

public enum FixAssetMachineryType
{
    /// <summary>
    /// مالکیت
    /// </summary>
    [Description("مالکیت")]
    ownership = 1,

    /// <summary>
    /// اجاره شده
    /// </summary>
    [Description("اجاره شده")]
    rented = 5,
}
