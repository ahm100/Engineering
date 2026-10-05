
namespace Engineering.Domain.Errors;

public static class FiduciaryProductErrors
{
    public static Error FiduciaryProductWithIdNotFound = new("NotFound", "درخواست امانی یافت نشد.", 404);
    public static Error CreatorIdNotFound = new("NotFound", "ثبت کننده درخواست امانی یافت نشد.", 404);

    public static Error InValidFiduciaryProductId = new("InvalidArguments", "درخواست امانی نامعتبر است.", 422);

    public static Error InValidCostCenter = new("InvalidArguments", "مرکز هزینه نامعتبر است.", 422);
    public static Error InValidProject = new("InvalidArguments", "پروژه نامعتبر است.", 422);
    public static Error InValidProjectOperation = new("InvalidArguments", "شرح عملیات پروژه نامعتبر است.", 422);
    public static Error InValidThirdPartyId = new("InvalidArguments", "طرف حساب نامعتبر است.", 422);
    public static Error InValidStatus = new("InvalidArguments", "وضعیت نامعتبر است.", 422);
    public static Error IValidStartDate = new("InvalidArguments", "از تاریخ معتبر نیست.", 422);
    public static Error IValidEndDate = new("InvalidArguments", "تا تاریخ معتبر نیست.", 422);
    public static Error InValidWarehouse = new("InvalidArguments", "انبار معتبر نیست.", 422);
    public static Error IsDeleted = new("NotFound", "درخواست کالای امانی حذف شده است.", 204);
    public static Error InValidCreator = new("InvalidArguments", "شما برای حذف این درخواست دسترسی ندارید.", 422);
    public static Error InValidConfirmedRequest = new("InvalidArguments", "تمامی کالا های این درخواست در حالت رد شده میباشند و نمیتوان این درخواست را تایید نمود.", 422);

}
