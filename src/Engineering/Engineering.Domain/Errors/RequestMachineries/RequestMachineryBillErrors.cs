
namespace Engineering.Domain.Errors;

public static class RequestMachineryBillErrors
{
    public static Error RequestMachineryBillNotFound = new("NotFound", " قبض درخواست ماشین آلات یافت نشد.", 404);
    public static Error RequestMachineryBillsNotFound = new("NotFound", "قبض های درخواست ماشین آلات یافت نشد.", 204);

    public static Error InValidRequestMachineryBill = new("InvalidArguments", " قبض درخواست ماشین آلات نامعتبر است.", 422);
    public static Error InValidRequestMachinery = new("InvalidArguments", "درخواست ماشین آلات نامعتبر است.", 422);
    public static Error InValidBillNumber = new("InvalidArguments", "شماره قبض نامعتبر است.", 422);
    public static Error InValidBillDate = new("InvalidArguments", "تاریخ قبض نامعتبر است.", 422);
    public static Error InValidcontractorId = new("InvalidArguments", "پیمانکار نامعتبر است.", 422);
    public static Error InValidFromDate = new("InvalidArguments", "تاریخ شروع نامعتبر است.", 422);
    public static Error InValidTodate = new("InvalidArguments", "تاریخ پایان نامعتبر است.", 422);
    public static Error IsDeleted = new("InvalidArguments", "این قبض حذف شده است.", 422);
    public static Error IsDuplicate = new("InvalidArguments", "قبض تکراری است.", 422);
}
