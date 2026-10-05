
namespace Engineering.Domain.Errors;

public static class RequestMachineryInquiryErrors
{
    public static Error RequestMachineryInquiryNotFOund = new("NotFound", "استعلام یافت نشد.", 404);
    public static Error RequestMachineryAssignmentNotFOund = new("NotFound", "مشخصه ماشین یافت نشد.", 404);

    public static Error InValidFromDate = new("InvalidArguments", "از تاریخ نامعتبر است.", 422);
    public static Error InValidToDate = new("InvalidArguments", "تا تاریخ نامعتبر است.", 422);
    public static Error InValidStatus = new("InvalidArguments", "وضعیت نامعتبر است.", 422);
    public static Error InValidThirdPartyId = new("InvalidArguments", "طرف حساب نامعتبر است.", 422);
    public static Error InValidCount = new("InvalidArguments", "تعداد نامعتبر است.", 422);
    public static Error InValidRequestedTime = new("InvalidArguments", "مدت زمان نامعتبر است.", 422);
    public static Error InValidUnit = new("InvalidArguments", "واحد نامعتبر است.", 422);
    public static Error InValidCurrency = new("InvalidArguments", "ارز نامعتبر است.", 422);
    public static Error InValidUnitPrice = new("InvalidArguments", "قیمت به ازای هر واحد نامعتبر است.", 422);
    public static Error InValidTotalPrice = new("InvalidArguments", "قیمت بر اساس درخواست نامعتبر است.", 422);
    public static Error InValidConfirmedUser = new("InvalidArguments", "کاربر تایید کننده نامعتبر است.", 422);
    public static Error InValidRequestMachinery = new("InvalidArguments", "درخواست ماشین آلات نامعتبر است.", 422);
    public static Error InValidRequestMachineryInquiry = new("InvalidArguments", "استعلام درخواست ماشین آلات نامعتبر است.", 422);
    public static Error InValidRequestMachineryInquiryOperator = new("InvalidArguments", "متصدی استعلام درخواست ماشین آلات نامعتبر است.", 422);
    public static Error InValidPrice = new("InvalidArguments", "حاصل ضرب تعداد در قیمت واحد در مدت زمان باید بزرگتر و یا مساوی قیمت نهایی باشد.", 422);
    public static Error CountIsInvalid = new("InvalidArguments", "تعداد میبایست کوچکتر یا برابر با تعداد درخواست باشد.", 422);
    public static Error IsDeleted = new("NotFound", "استعلام درخواست ماشین آلات حذف شده است.", 204);
    public static Error UnitPriceCanNotGreater = new("InvalidArguments", "قیمت واحد نامعتبر است این قیمت نمی تواند بیشتر از 16 رقم باشد.", 422);
    public static Error TotalPriceCanNotGreater = new("InvalidArguments", "قیمت نهایی نامعتبر است این قیمت نمی تواند بیشتر از 16 رقم باشد.", 422);

}
