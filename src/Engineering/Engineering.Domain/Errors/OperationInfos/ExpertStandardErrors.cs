
namespace Engineering.Domain.Errors;

public static class ExpertStandardErrors
{
    public static Error ExpertWithIdNotFound = new("NotFound", "هیچ متخصص ای با این شناسه یافت نشد.", 404);
    public static Error NameIsDuplicate = new("Duplicate", "نام متخصص تکراری است.", 409);
    public static Error ExpertIsDuplicate = new("Duplicate", "متخصص انتخابی برای این شرح عملیات وجود دارد.", 409);
    public static Error CantDelete = new("Duplicate", "شما نمیتوانید استاندارد ایجاد نشده را حذف کنید.", 409);
    public static Error CodeIsDuplicate = new("Duplicate", "کد متخصص تکراری است.", 409);
    public static Error UnValidId = new("InvalidArguments", "شناسه متخصص نامعتبر است.", 422);
    public static Error IsDeleted = new("NotFound", "این متخصص حذف شده است.", 204);
    public static Error IsActive = new("InvalidArguments", "وضعیت متخصص فعال است.", 422);
    public static Error IsInactive = new("InvalidArguments", "وضعیت متخصص غیرفعال است.", 422);
    public static Error ExpertsNotFound = new("NotFound", "هیچ متخصص ای با این مشخصه عملیاتی یافت نشد.", 204);
    public static Error MoreThan59Min = new("InvalidArguments", "شما نمیتوانین دقیقه را بیشتر از 59 وارد کنید.", 422);

    public static Error ExpertUnitIdIsEmpty = new("InvalidArguments", "شناسه واحد متخصص خالی است!", 422);
    public static Error ExpertNumberIsEmpty = new("InvalidArguments", "مقدار متخصص خالی است!", 422);
    public static Error UnusedPercentageIsEmpty = new("InvalidArguments", "درصد استفاده خالی است!", 422);
    public static Error TimeSpantIsEmpty = new("InvalidArguments", "زمان مصرفی خالی است!", 422);
    public static Error ExpertIdIsEmpty = new("InvalidArguments", "شناسه متخصص خالی است!", 422);

    public static Error OprationInfoIdIsEmpty = new("InvalidArguments", "شناسه شرح عملیات خالی است!", 422);

}