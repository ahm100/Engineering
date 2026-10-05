namespace Engineering.Domain.Errors.Adjustments;

public static class AdjustmentErrors
{
    public static Error DuplicateCode = new("Duplicate", "تعدیل تکراری است.", 409);
    public static Error AdjustmentReferenceNotFound = new("NotFound", "هیچ تعدیلی با این منبع یافت نشد.", 404);
    public static Error AdjustmentIndexNotFound = new("NotFound", "هیچ تعدیلی با این شناسه یافت نشد.", 404);
    public static Error AdjustmentIndexValueNotFound = new("NotFound", " مقداری با این مشخصات یافت نشد.", 404);
}