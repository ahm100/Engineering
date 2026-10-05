
namespace Engineering.Domain.Errors;

public static class MachineryStandardErrors
{
    public static Error MachineryWithIdNotFound = new("NotFound", "هیچ ماشین آلات و تجهیزات با این شناسه یافت نشد.", 404);
    public static Error NameIsDuplicate = new("Duplicate", "نام ماشین آلات و تجهیزات تکراری است.", 409);
    public static Error MachineryIsDuplicate = new("Duplicate", "ماشین آلات و ابزار انتخابی برای این شرح عملیات وجود دارد.", 409);
    public static Error CodeIsDuplicate = new("Duplicate", "کد ماشین آلات و تجهیزات تکراری است.", 409);
    public static Error CantDelete = new("Duplicate", "شما نمیتوانید استاندارد ایجاد نشده را حذف کنید.", 409);
    public static Error UnValidId = new("InvalidArguments", "ماشین آلات و تجهیزات با این شناسه یافت نشد.", 422);
    public static Error MoreThan59Min = new("InvalidArguments", "شما نمیتوانین دقیقه را بیشتر از 59 وارد کنید.", 422);
    public static Error TimeSpantCountError = new("InvalidArguments", "زمان مصرفی باید با فرمت 00:00 وارد شود.", 422);
    public static Error IsDeleted = new("NotFound", "این ماشین آلات و تجهیزات حذف شده است.", 204);
    public static Error IsActive = new("InvalidArguments", "ماشین آلات و تجهیزات فعال است.", 422);
    public static Error IsInactive = new("InvalidArguments", "ماشین آلات و تجهیزات غیرفعال است.", 422);
    public static Error MachineriesNotFound = new("NotFound", "شرح عملیات انتخابی هیچ ماشین آلات و تجهیزات ندارد.", 204);

    public static Error MachineryUnitIdIsEmpty = new("InvalidArguments", "شناسه واحد ماشین آلات و تجهیزات خالی است!", 422);
    public static Error MachineryNumberIsEmpty = new("InvalidArguments", "مقدار ماشین آلات و تجهیزات خالی است!", 422);
    public static Error UnusedPercentageIsEmpty = new("InvalidArguments", "درصد استفاده خالی است!", 422);
    public static Error TimeSpantIsEmpty = new("InvalidArguments", "زمان مصرفی خالی است!", 422);
    public static Error MachineryIdIsEmpty = new("InvalidArguments", "شناسه ماشین آلات و تجهیزات خالی است!", 422);
    public static Error OprationInfoIdIsEmpty = new("InvalidArguments", "شناسه شرح عملیات خالی است!", 422);

}