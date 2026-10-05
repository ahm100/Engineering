
namespace Engineering.Domain.Errors;

public static class MachineryErrors
{
    public static Error NameIsDuplicate = new("Duplicate", "نام ماشین آلات و تجهیزات تکراری است.", 409);
    public static Error CodeIsDuplicate = new("Duplicate", "کد ماشین آلات و تجهیزات تکراری است.", 409);

    public static Error IsActive = new("InvalidArguments", "وضعیت ماشین آلات و تجهیزات فعال میباشد.", 422);
    public static Error IsInactive = new("InvalidArguments", "وضعیت ماشین آلات و تجهیزات غیرفعال میباشد.", 422);

    public static Error MachineryMachineriesGroupNotFound = new("NotFound", "گروه ماشین آلات و تجهیزات انتخابی یافت نشد.", 204);
    public static Error MachineryGroupNotFoundWithCode = new("NotFound", "برای ماشین آلات شما گروهی یافت نشد.", 404);
    public static Error MachineryWithIdNotFound = new("NotFound", "هیچ ماشین آلات و تجهیزات با این شناسه یافت نشد.", 404);
    public static Error MachineryWithCodeNotFound = new("NotFound", "هیچ ماشین آلات و تجهیزات ای با این کد یافت نشد.", 404);
    public static Error MachineryWithNameNotFound = new("NotFound", "هیچ ماشین آلات و تجهیزات با این نام یافت نشد.", 404);
    public static Error FilteredMachineryNotFound = new("NotFound", "هیچ ماشین آلات و تجهیزات با این اطلاعات یافت نشد.", 204);
    public static Error MachineryChildNotFound = new("NotFound", "ماشین آلات و تجهیزات انتخابی زیرشاخه ای ندارد.", 204);
    public static Error IsDeleted = new("NotFound", "این ماشین آلات و تجهیزات حذف شده است.", 204);
    public static Error CanNotDelete = new("NotAccess", "به دلیل داشتن وابستگی اطلاعاتی نمیتوانین این ماشین آلات و تجهیزات را حذف کنید.", 422);
    public static Error CanNotDeleteBecauseOfStandards = new("NotAccess", "به دلیل داشتن استاندارد مصرفی نمیتوانید این ماشین آلات و تجهیزات را حذف کنید.", 422);
    public static Error CanNotDeleteBecauseOfVolumes = new("NotAccess", "به دلیل داشتن احجام مصرفی نمیتوانید این ماشین آلات و تجهیزات را حذف کنید.", 422);

    public static Error UnValidId = new("InvalidArguments", "ماشین آلات و تجهیزات با این شناسه یافت نشد.", 422);
    public static Error UnValidData = new("InvalidArguments", "اطلاعات وارد شده نامعتبر است.", 422);
    public static Error IdIsEmpty = new("InvalidArguments", "شناسه خالی است!", 422);
    public static Error MachineryNameIsEmpty = new("InvalidArguments", "نام خالی است!", 422);
    public static Error MachineryCodeIsEmpty = new("InvalidArguments", "کد خالی است!", 422);
    public static Error IsActiveIsEmpty = new("InvalidArguments", "وضعیت خالی است!", 422);
    public static Error MachineriesGroupIsEmpty = new("InvalidArguments", "گروه ماشین خالی است!", 422);

    public static Error HaveChild = new("InvalidArguments", "این ماشین آلات و تجهیزات دارای وابستگی اطلاعاتی است و قابل تغییر نیست.", 422);

}