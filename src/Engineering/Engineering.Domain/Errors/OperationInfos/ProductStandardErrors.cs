
namespace Engineering.Domain.Errors;

public static class ProductStandardErrors
{
    public static Error ProductWithIdNotFound = new("NotFound", "هیچ کالایی با این شناسه یافت نشد.", 404);
    public static Error CategoryWithIdNotFound = new("NotFound", "هیچ دسته بندی با این شناسه یافت نشد.", 404);
    public static Error NameIsDuplicate = new("Duplicate", "نام کالا تکراری است.", 409);
    public static Error CodeIsDuplicate = new("Duplicate", "کد کالا تکراری است.", 409);
    public static Error ProductIsDuplicate = new("Duplicate", "کالای انتخابی برای این شرح عملیات وجود دارد.", 409);
    public static Error CategoryIsDuplicate = new("Duplicate", "دسته بندی انتخابی برای این شرح عملیات وجود دارد.", 409);
    public static Error UnValidId = new("InvalidArguments", "شناسه کالا نامعتبر است.", 422);
    public static Error UnValidType = new("InvalidArguments", "نوع انتخابی نامعتبر است.", 422);
    public static Error IsDeleted = new("NotFound", "این کالا حذف شده است.", 204);
    public static Error IsActive = new("InvalidArguments", "کالا فعال است.", 422);
    public static Error IsInactive = new("InvalidArguments", "کالا غیرفعال است.", 422);
    public static Error ProductNotFound = new("NotFound", "شرح عملیات انتخابی چنین کالایی در خود ندارد.", 204);
    public static Error CantDelete = new("Duplicate", "شما نمیتوانید استاندارد ایجاد نشده را حذف کنید.", 409);

    public static Error ProductUnitIdIsEmpty = new("InvalidArguments", "شناسه واحد کالا خالی است!", 422);
    public static Error TypeIsEmpty = new("InvalidArguments", "نوع استاندارد کالا خالی است!", 422);
    public static Error ProductNumberIsEmpty = new("InvalidArguments", "مقدار کالا خالی است!", 422);
    public static Error ProductIdIsEmpty = new("InvalidArguments", "شناسه کالا خالی است!", 422);
    public static Error UnusedPercentageIsEmpty = new("InvalidArguments", "درصد استفاده خالی است!", 422);
    public static Error TimeSpantIsEmpty = new("InvalidArguments", "زمان مصرفی خالی است!", 422);
    public static Error OprationInfoIdIsEmpty = new("InvalidArguments", "شناسه شرح عملیات خالی است!", 422);
}