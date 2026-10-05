
namespace Engineering.Domain.Errors;

public static class RequestContractorInquiryErrors
{
    public static Error RequestContractorInquiryNotFound = new("NotFound", "استعلام یافت نشد.", 404);
    public static Error RequestContractorInquiriesNotFound = new("NotFound", "استعلام ها یافت نشد.", 204);

    public static Error UnvalidContractorId = new("InvalidArguments", "پیمانکار نامعتبر است.", 422);
    public static Error InValidCurrencyId = new("InvalidArguments", "ارز نامعتبر است.", 422);
    public static Error InValidAmount = new("InvalidArguments", "مبلغ استعلام نامعتبر است.", 422);
    public static Error InValidTotalAmount = new("InvalidArguments", "مبلغ کل استعلام نامعتبر است.", 422);
    public static Error InValidType = new("InvalidArguments", "نوع خدمت استعلام نامعتبر است.", 422);
    public static Error InValidRequestContractor = new("InvalidArguments", "درخواست پیمانکار نامعتبر است.", 422);
    public static Error InValidRequestContractorInquiry = new("InvalidArguments", "استعلام درخواست پیمانکار نامعتبر است.", 422);
    public static Error IsDeleted = new("InvalidArguments", "استعلام از قبل حذف شده است.", 422);
    public static Error InvalidList = new("InvalidArguments", "لیست استعلام هانامعتبر است.", 422);
    public static Error InvalidId = new("InvalidArguments", "شناسه استعلام نامعتبر است.", 422);
}
