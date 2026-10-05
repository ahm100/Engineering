namespace Engineering.Domain.Errors.RequestGoodsSupplies;

public static class GoodsManagerAssignmentErrors
{
    public static Error NotFound => new(
        "NotFound",
        "تخصیص مدیر کالا با این شناسه یافت نشد.",
        404);

    public static Error DuplicateAssignment => new(
        "InvalidArguments",
        "این مدیر کالا قبلاً برای این محصول/گروه/دسته بندی تخصیص داده شده است.",
        422);

    public static Error InvalidGoodsManagerThirdParty => new(
        "InvalidArguments",
        "مدیر کالا (طرف حساب) معتبر نیست.",
        422);

    public static Error AtLeastOneScopeRequired => new(
    "InvalidArguments",
    "حداقل یکی از شناسه‌های دسته‌بندی، گروه یا محصول باید وارد شود.",
    422);

    public static Error OrganizationNotFound => new(
        "NotFound",
        "واحد سازمانی یافت نشد.",
        404);

    public static Error GroupNotFound => new(
        "NotFound",
        "گروه محصول یافت نشد.",
        404);

    public static Error ProductNotFound => new(
        "NotFound",
        "محصول یافت نشد.",
        404);

    public static Error GoodsManagerNotFound => new(
        "NotFound",
        "مدیر کالا (طرف حساب) یافت نشد.",
        404);

    public static Error HistoryNotFound => new(
        "NotFound",
        "تاریخچه‌ای برای این تخصیص یافت نشد.",
        404);
}