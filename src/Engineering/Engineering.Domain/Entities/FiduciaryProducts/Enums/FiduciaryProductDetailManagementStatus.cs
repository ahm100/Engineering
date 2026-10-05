
namespace Engineering.Domain.Entities.FiduciaryProducts.Enums;

public enum FiduciaryProductDetailManagementStatus
{
    [Description("تامین شده")]
    Approved = 1,
    [Description("رد شده")]
    Rejected = 2,
    [Description("در انتظار بررسی")]
    Pending = 3,
}
