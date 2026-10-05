
namespace Engineering.Domain.Errors;

public static class CostCenterVirtualGroupErrors
{
    public static Error CostCenterVirtualGroupNotFound = new("NotFound", "گروه مجازی یافت نشد.", 404);

    public static Error InValidCostCenterVirtualGroup = new("InvalidArguments", "گروه مجازی نامعتبر است.", 422);
    public static Error InValidTitle = new("InvalidArguments", "عنوان گروه کانال نامعتبر است.", 422);
    public static Error InValidLink = new("InvalidArguments", "لینک نامعتبر است.", 422);
    public static Error InValidIdentifier = new("InvalidArguments", "شناسه نامعتبر است.", 422);
    public static Error InValidSendToday = new("InvalidArguments", "ارسال امروز نامعتبر است.", 422);
    public static Error InValidTodayTime = new("InvalidArguments", "ساعت ارسال امروز نامعتبر است.", 422);
    public static Error InValidSendYesterday = new("InvalidArguments", "ارسال دیروز نامعتبر است.", 422);
    public static Error InValidYesterdayTime = new("InvalidArguments", "ساعت دیروز نامعتبر است.", 422);
    public static Error InValidCostCenter = new("InvalidArguments", "مرکز هزینه نامعتبر است.", 422);
    public static Error IsDeleted = new("NotFound", "گروه مرکز هزینه مجازی قابل حذف نمیباشد.", 204);
}
