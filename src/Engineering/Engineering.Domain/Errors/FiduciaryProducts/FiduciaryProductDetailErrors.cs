
namespace Engineering.Domain.Errors;

public static class FiduciaryProductDetailErrors
{
    public static Error FiduciaryProductDetailWithIdNotFound = new("NotFound", "کالای درخواست امانی یافت نشد.", 404);

    public static Error InValidFiduciaryProductDetail = new("InvalidArguments", "کالای درخواست امانی نامعتبر است.", 422);
    public static Error InValidFiduciaryProductDetailId = new("InvalidArguments", "شناسه کالای درخواست امانی نامعتبر است.", 422);
    public static Error InValidConfirmLoanDays = new("InvalidArguments", "تعداد روز تاییدیه امانی نامعتبر است.", 422);
    public static Error InValidConfirmDailyLateFine = new("InvalidArguments", "مقدار جریمه تاییدیه امانی نامعتبر است.", 422);
    public static Error InValidProductId = new("InvalidArguments", "شناسه کالا نامعتبر است.", 422);
    public static Error InValidMeasureUnitId = new("InvalidArguments", "شناسه واحد اندازه گیری نامعتبر است.", 422);
    public static Error InValidCurrencyId = new("InvalidArguments", "شناسه واحد ارز نامعتبر است.", 422);
    public static Error InValidStatus = new("InvalidArguments", "وضعیت کالا نامعتبر است.", 422);
    public static Error InValidDeleteStatus = new("InvalidArguments", "کالا جهت حذف دارای وضعیت نامعتبر است.", 422);
    public static Error IsDeleted = new("NotFound", "شرح درخواست کالای امانی حذف شده است.", 204);
}
