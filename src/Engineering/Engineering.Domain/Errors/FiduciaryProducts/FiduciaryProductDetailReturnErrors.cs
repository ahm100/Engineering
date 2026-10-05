
namespace Engineering.Domain.Errors;

public static class FiduciaryProductDetailReturnErrors
{
    public static Error FiduciaryProductDetailReturnWithIdNotFound = new("NotFound", "عودت کالای درخواست امانی یافت نشد.", 404);

    public static Error InValidFiduciaryProductDetail = new("InvalidArguments", "عودت کالای درخواست امانی نامعتبر است.", 422);
    public static Error InValidFiduciaryProductDetailReturn = new("InvalidArguments", "کالای درخواست امانی نامعتبر است.", 422);
    public static Error InValidCurrencyId = new("InvalidArguments", "شناسه واحد ارز نامعتبر است.", 422);
    public static Error InValidType = new("InvalidArguments", "نوع عودت نامعتبر است.", 422);
    public static Error InValidReturnDate = new("InvalidArguments", "تاریخ عودت نامعتبر است.", 422);
    public static Error InValidReturnCount = new("InvalidArguments", "تعداد عودت نامعتبر است.", 422);
    public static Error InValidLateDay = new("InvalidArguments", "تعداد روز دیرکرد نامعتبر است.", 422);
    public static Error InValidLateFine = new("InvalidArguments", "دیر کرد جریمه نامعتبر است.", 422);

    public static Error InValidCount = new("InvalidArguments", "تعداد عودت بیشتر از امانی است.", 422);
    public static Error IsDeleted = new("NotFound", "عودت کالای امانی حذف شده است", 204);
    public static Error LateFineCanNotGreater = new("InvalidArguments", "جریمه دیرکرد نامعتبر است این قیمت نمی تواند بیشتر از 16 رقم باشد.", 422);

}
