namespace Engineering.Domain.Errors.EngineeringConfigs;

public static class EngineeringConfigErrors
{
    public static Error ActiveConfigNotFound = new("InvalidArguments", "کانفیگ فعال برای سازمان پیدا نشد.", 422);
    public static Error FilteredConfigNotFound = new("InvalidArguments", "کانفیگ با پارامترهای مدنظر پیدا نشد.", 422);
    public static Error ConfigWithIdNotFound = new("InvalidArguments", "کانفیگ با شناسه پیدا نشد.", 422);
    public static Error CodeConfigWithIdNotFound = new("InvalidArguments", "الگوریتم کد گذاری با شناسه پیدا نشد.", 422);

    public static Error ProjectThirdPartyIsFalse = new("InvalidArguments", "پروژه های شما در کانفیگ مهندسی شما دارای دسترسی یوزر نمیباشد.", 422);

    public static Error NoConfigHistoryFound = new("NotFound", "تاریخچه ای برای کانفیگ مدنظر پیدا نشد.", 204);
}