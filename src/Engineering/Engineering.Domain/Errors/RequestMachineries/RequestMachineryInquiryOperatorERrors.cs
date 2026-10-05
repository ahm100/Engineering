
namespace Engineering.Domain.Errors;

public static class RequestMachineryInquiryOperatorErrors
{
    public static Error RequestMachineryInquiryOperatorNotFound = new("NotFound", "متصدی درخواست ماشین آلات یافت نشد.", 404);

    public static Error DuplicateRequestMachineryInquiryOperator = new("Conflict", "متصدی پیمانکار تکراری است لطفا فعال شود.", 409);

    public static Error InValidIsActive = new("InvalidArguments", "وضعیت فعال نامعتبر است.", 422);
    public static Error InValidOperatorId = new("InvalidArguments", "متصدی نامعتبر است.", 422);
    public static Error InValidRequestMachinery = new("InvalidArguments", "درخواست ماشین آلات نامعتبر است.", 422);
    public static Error InValidRequestMachineryInquiryOperator = new("InvalidArguments", "متصدی درخواست ماشین آلات نامعتبر است.", 422);
    public static Error InValidRequestMachineryInquiryOperatorInquiries = new("InvalidArguments", "متصدی درخواست ماشین آلات استعلام ثبت شده دارد.", 422);
    public static Error IsDeleted = new("NotFound", "متصدی استعلام درخواست ماشین آلات حذف شده است.", 204);
}
