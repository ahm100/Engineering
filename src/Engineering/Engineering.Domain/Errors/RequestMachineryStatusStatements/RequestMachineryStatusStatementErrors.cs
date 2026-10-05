
namespace Engineering.Domain.Errors;

public static class RequestMachineryStatusStatementErrors
{
    public static Error RequestMachineryStatusStatementWithIdNotFound = new("NotFound", "صورت وضعیت درخواست ماشین آلات یافت نشد.", 404);
    public static Error RequestMachineryStatusStatementNotFound = new("NotFound", "صورت وضعیت درخواست ماشین آلات با این اطلاعات یافت نشد.", 204);

    public static Error InValidId = new("InvalidArguments", "شناسه نامعتبر است.", 422);
    public static Error InValidPettyCash = new("InvalidArguments", "درصورت انتخاب تنخواه گردان انتخاب شخص تنخواه گردان اجباریست.", 422);
    public static Error InValidstatusStatement = new("InvalidArguments", "صورت وضعیت درخواست های ماشین آلات نامعتبر است.", 422);
    public static Error InValidContractorId = new("InvalidArguments", "شناسه پیمانکار نامعتبر است.", 422);
    public static Error InValidSeasonId = new("InvalidArguments", "شناسه فصل نامعتبر است.", 422);
    public static Error InValidTotalRequestedCount = new("InvalidArguments", "تعداد درخواست نامعتبر است.", 422);
    public static Error InValidTotalFinalPrice = new("InvalidArguments", "مبلغ محاسبه شده نامعتبر است.", 422);
    public static Error InValidUnit = new("InvalidArguments", "واحد نامعتبر است.", 422);
    public static Error InValidStatus = new("InvalidArguments", "وضعیت نامعتبر است.", 422);
    public static Error InValidRequestMachinery = new("InvalidArguments", "شناسه درخواست ماشین آلات نامعتبر است.", 422);
    public static Error InValidRequestMachineryStatusStatement = new("InvalidArguments", "شناسه صورت وضعیت نامعتبر است.", 422);
    public static Error InValidProject = new("InvalidArguments", "شناسه پروژه نامعتبر است.", 422);
    public static Error InValidStatementForDate = new("InvalidArguments", "در این بازه تاریخی به پیمانکار پرداختی صورت گرفته است.", 422);
    public static Error InValidCostCenter = new("InvalidArguments", "شناسه مرکز هزینه نامعتبر است.", 422);
    public static Error InValidMachinery = new("InvalidArguments", "شناسه ماشین آلات نامعتبر است.", 422);
    public static Error InValidPaymentOrder = new("InvalidArguments", "شناسه دستور پرداخت نامعتبر است.", 422);
    public static Error InValidRequestMachhineryDate(string number) => new("InvalidArguments", $"دستور پرداخت برای درخواست {number} قبلا ایجاد شده است.", 422);
}
