
namespace Engineering.Domain.Errors;

public static class CostCenterAuthorizedRoleErrors
{
    public static Error CostCenterAuthorizedRoleWithIdNotFound = new("NotFound", "نقش مجاز مرکزهزینه ای با این شناسه یافت نشد.", 404);
    public static Error UnValidId = new("InvalidArguments", "نقش مجاز مرکزهزینه با این شناسه نامعتبر است.", 422);
    public static Error AuthorizedRoleIdIsEmpty = new("InvalidArguments", "نقش مجاز مرکزهزینه خالی است.", 422);
    public static Error IsDeleted = new("NotFound", "نقش مجاز مرکزهزینه حذف شده است.", 204);
    public static Error WithIdNotFound = new("NotFound", "نقش مجاز مرکزهزینه ای با این شناسه یافت نشد.", 404);
    public static Error DataIsNull = new("NotFound", "هیچ نقش مجازی برای این مرکز هزینه وجود ندارد.", 204);
    public static Error UnValidAuthorizedRoles = new("InvalidArguments", "شناسه ای از نقش های مجاز درست نمیباشد.", 422);
    public static Error UnValidAuthorizedRole = new("InvalidArguments", "شناسه نقش مجاز درست نمیباشد.", 422);
    public static Error CostCenterAuthorizedRoleNotFound = new("NotFound", "قرارداد کارفرمای انتخابی زیرشاخه ای ندارد.", 204);
}