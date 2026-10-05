namespace Engineering.Domain.Errors.Projects;

public static class SubProjectErrors
{
    public static Error NotFound = new("NotFound", "زیرپروژه یافت نشد.", 404);
    public static Error ParentNotUsable = new("InvalidArguments", "پروژه والد فعال یا قابل استفاده نیست.", 422);
    public static Error NoProjectAccess = new("NotAccess", "شما به پروژه والد دسترسی ندارید.", 403);
    public static Error InvalidDates = new("InvalidArguments", "تاریخ پایان نمیتواند قبل از تاریخ شروع باشد.", 422);
    public static Error InvalidStatusTransition = new("InvalidArguments", "فقط تغییر وضعیت از پیشنویس به فعال و سپس به تکمیلشده مجاز است.", 422);
    public static Error HasDependencies = new("InvalidArguments", "به دلیل وجود اطلاعات وابسته، این زیرپروژه قابل حذف نیست.", 422);
    public static Error DuplicateCode = new("Duplicate", "زیرپروژهای با کد تولیدشده از قبل وجود دارد.", 409);
}