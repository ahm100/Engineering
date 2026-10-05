
namespace Engineering.Domain.Errors;

public static class CostCenterInformedUserErrors
{
    public static Error CostCenterInformedUseWithIdNotFound = new("NotFound", "هیچ کاربر آگاه مرکزهزینه ای با این شناسه یافت نشد.", 404);
    public static Error UnValidId = new("InvalidArguments", "کاربر آگاه مرکزهزینه با این شناسه نامعتبر است.", 422);
    public static Error IsDeleted = new("NotFound", "کاربر آگاه مرکزهزینه حذف شده است.", 204);
    public static Error WithIdNotFound = new("NotFound", "هیچ کاربر آگاه مرکزهزینه ای با این شناسه یافت نشد.", 404);
    public static Error CostCenterInformedUseNotFound = new("NotFound", "قرارداد کارفرمای انتخابی زیرشاخه ای ندارد.", 204);
    public static Error DataIsNull = new("NotFound", "هیچ کاربر مطلع برای این مرکز هزینه وجود ندارد.", 204);
    public static Error EmployeeIdIsEmpty = new("InvalidArguments", "شناسه کاربر مطلع مرکزهزینه خالی است.", 422);
}
