
namespace Engineering.Domain.Errors;

public static class RequestRewardErrors
{
    public static Error RequestRewardWithIdNotFound = new("NotFound", "هیچ درخواست پاداش و جریمه ای با این شناسه یافت نشد.", 404);
    public static Error ProjectOperationDetailWithIdNotFound = new("NotFound", "هیچ ریز متر با این شناسه یافت نشد.", 204);

    public static Error InValidRequestRewardId = new("InvalidArguments", "کد درخواست جریمه پاداش خالی میباشد!", 422);
    public static Error InValidThirdPartyIds = new("InvalidArguments", "شناسه طرف حساب نامعتبر است.", 422);
    public static Error InValidCurrencyId = new("InvalidArguments", "شناسه ارز نامعتبر است.", 422);
    public static Error InValidRequestRewardStatus = new("InvalidArguments", "وضعیت پاداش و جریمه درست نیست.", 422);
    public static Error InValidStatusForDelete = new("InvalidArguments", "وضعیت پاداش و جریمه تایید شده است و پاک نمیشود.", 422);
    public static Error InValidStartDate = new("InvalidArguments", "از تاریخ معتبر نیست.", 422);
    public static Error InValidEndDate = new("InvalidArguments", "تا تاریخ معتبر نیست.", 422);
    public static Error InValidOperationDetailIds = new("InvalidArguments", "شرح عملیات معتبر نیست.", 422);
    public static Error InValidProjectOperationDetail = new("InvalidArguments", "ریز متره معتبر نیست.", 422);
    public static Error InValidType = new("InvalidArguments", "نوع معتبر نیست.", 422);
    public static Error InValidProductPrices = new("InvalidArguments", "مبلغ جریمه از مبالغ محصولات کمتر است.", 422);
    public static Error InValidStatus = new("InvalidArguments", "وضعیت معتبر نیست.", 422);
    public static Error InValidOfferedPrice = new("InvalidArguments", "قیمت پیشنهادی نامعتبر است قیمت نمی تواند کوچکتر مساوی صفر باشد.", 422);
    public static Error OfferedPriceCanNotGreater = new("InvalidArguments", "قیمت پیشنهادی نامعتبر است قیمت نمی تواند بیشتر از 16 رقم باشد.", 422);
    public static Error ConfirmedPriceCanNotGreater = new("InvalidArguments", "قیمت تاییدشده نامعتبر است قیمت نمی تواند بیشتر از 16 رقم باشد.", 422);
    public static Error InValidConfirmedPrice = new("InvalidArguments", "قیمت تاییدشده نامعتبر است قیمت نمی تواند کوچکتر مساوی صفر باشد.", 422);
    public static Error InValidDescription = new("InvalidArguments", "توضیحات نامعتبر است.", 422);
    public static Error InValidRegistrationDate = new("InvalidArguments", "تاریخ نامعتبر است.", 422);
    public static Error InValidCostCenter = new("InvalidArguments", "مرکز هزینه نامعتبر است.", 422);
    public static Error InValidData = new("InvalidArguments", "ورودی نامعتبر است.", 422);
    public static Error IdIsEmptyForDelete = new("InvalidArguments", "شناسه پاداش وجریمه برای حذف خالی است.", 422);
    public static Error IdIsEmptyForUpdate = new("InvalidArguments", "شناسه پاداش وجریمه برای ویرایش خالی است.", 422);
    public static Error IdIsRelatedToCSS = new("InvalidArguments", "است به  اضافات یا کسورات صورت وضعیت فعال متصل .", 422);
    public static Error IsDeleted = new("NotFound", "پاداش و جریمه حذف شده است.", 204);

    public static Error CostCenterIdIsInValide = new("InvalidArguments", "شناسه مرکزهزینه برای این ریزمتره صحیح نمیباشد.", 422);
    public static Error ProjectIdIsInValide = new("InvalidArguments", "شناسه پروژه برای این ریزمتره صحیح نمیباشد.", 422);
    public static Error ProjectIsInActive = new("InvalidArguments", "پروژه فعال نمیباشد.", 422);
    public static Error ProjectOperationIdIsInValide = new("InvalidArguments", "شناسه شرح عملیات پروژه برای این ریزمتره صحیح نمیباشد.", 422);
}
