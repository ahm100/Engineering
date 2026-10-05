
namespace Engineering.Domain.Errors;

public static class DailyProjectOperationProductErrors
{
    public static Error DailyProjectOperationProductWithIdNotFound = new("NotFound", "عملکرد روزانه یافت نشد.", 404);

    public static Error InValidProductGroupId = new("InvalidArguments", "گروه محصول نامعتبر است.", 422);
    public static Error InValidProductId = new("InvalidArguments", "محصول نامعتبر است.", 422);
    public static Error InValidFinalValue = new("InvalidArguments", "مقدار نامعتبر است.", 422);
    public static Error InValidDailyProjectOperation = new("InvalidArguments", "عملکرد روزانه نامعتبر است.", 422);
    public static Error InValidConsumableVolumeProduct = new("InvalidArguments", "مصرفی ریز متر نامعتبر است.", 422);
    public static Error IsDeleted = new("NotFound", "کالای عملکرد روزانه حذف شده است.", 204);
}
