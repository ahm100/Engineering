
namespace Engineering.Domain.Errors;

public static class CostCenterAuthorizedUserErrors
{
    public static Error CostCenterAuthorizedUserWithIdNotFound = new("NotFound", "هیچ کاربر مجاز مرکزهزینه ای با این شناسه یافت نشد.", 404);
    public static Error UnValidId = new("InvalidArguments", "شناسه کاربر مجاز مرکزهزینه نامعتبر است.", 422);
    public static Error IsDeleted = new("NotFound", "کاربر مجاز مرکزهزینه حذف شده است.", 204);
    public static Error WithIdNotFound = new("NotFound", "هیچ کاربر مجاز مرکزهزینه ای با این شناسه یافت نشد.", 404);
    public static Error UnValidAuthorizedUser = new("InvalidArguments", "شناسه کاربر مجاز درست نمیباشد.", 422);
    public static Error CostCenterAuthorizedUserNotFound = new("NotFound", "کاربر مجاز مرکزهزینه دارای وابستگی اطلاعاتی است و قابل تغییر نیست.", 204);
    public static Error DataIsNull = new("NotFound", "هیچ کاربر مجازی برای این مرکز هزینه وجود ندارد.", 204);
    public static Error DatasIsNull = new("NotFound", "کاربری با نقش های مشخص شده در مرکزهزینه یافت نشد.", 204);
    public static Error UserIdIsEmpty = new("InvalidArguments", "شناسه کاربر مجاز مرکزهزینه خالی است.", 422);
}
