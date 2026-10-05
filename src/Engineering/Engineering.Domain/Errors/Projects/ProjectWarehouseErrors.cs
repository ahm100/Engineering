namespace Engineering.Domain.Errors;

public static class ProjectWarehouseErrors
{
    public static Error ProjectNotFound = new("NotFound", "هیچ پروژه ای با این شناسه یافت نشد.", 404);
    public static Error WarehouseNotFound = new("InvalidArguments", "هیچ انباری با این شناسه یافت نشد.", 422);
    public static Error Duplicate = new("Duplicate", "انبار انتخابی از قبل برای پروژه ثبت شده است.", 409);
    public static Error NotFound = new("NotFound", "هیچ انبار پروژه ای با این شناسه یافت نشد.", 404);
    public static Error DefaultCannotBeDeleted = new("InvalidArguments", "انبار پیش فرض پروژه قابل حذف نیست.", 422);
    public static Error DefaultCannotBeUnset = new("InvalidArguments", "ابتدا انبار پیش فرض جدید پروژه را انتخاب کنید.", 422);
    public static Error MoreThanOneDefault = new("InvalidArguments", "بیش از یک انبار پیش فرض برای پروژه انتخاب شده است.", 422);
    public static Error NoDefault = new("InvalidArguments", "یک انبار پیش فرض برای پروژه انتخاب کنید.", 422);
    public static Error DataNotFound = new("NotFound", "هیچ انباری برای پروژه یافت نشد.", 204);
}
