namespace Engineering.Domain.Entities.FiduciaryProducts.Enums;

public enum FiduciaryProductDetailReturnType
{
    [Description("عودت سالم")]
    SafeReturned = 1,
    [Description("عودت ضایعات")]
    WasteReturned = 2,
    [Description("عودت سوخته")]
    BurnedReturned = 3,
}
