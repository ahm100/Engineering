
namespace Engineering.Domain.Errors;

public static class MachineriesGroupErrors
{
    public static Error NameIsDuplicate = new("Duplicate", "نام گروه ماشین آلات و تجهیزات تکراری است.", 409);
    public static Error CodeIsDuplicate = new("Duplicate", "کد گروه ماشین آلات و تجهیزات تکراری است.", 409);

    public static Error IsActive = new("InvalidArguments", "وضعیت گروه ماشین آلات و تجهیزات فعال میباشد.", 422);
    public static Error IsInactive = new("InvalidArguments", "وضعیت گروه ماشین آلات و تجهیزات غیرفعال میباشد.", 422);

    public static Error MachineriesGroupWithIdNotFound = new("NotFound", "هیچ گروه ماشین آلات و تجهیزات با این شناسه یافت نشد.", 404);
    public static Error MachineriesGroupWithCodeNotFound = new("NotFound", "هیچ گروه ماشین آلات و تجهیزات با این کد یافت نشد.", 404);
    public static Error MachineriesGroupWithNameNotFound = new("NotFound", "هیچ گروه ماشین آلات و تجهیزات با این نام یافت نشد.", 404);
    public static Error FilteredMachineriesGroupNotFound = new("NotFound", "هیچ گروه ماشین آلات و تجهیزات با این اطلاعات یافت نشد.", 204);
    public static Error MachineriesGroupChildNotFound = new("NotFound", "این گروه ماشین آلات و تجهیزات دارای زیرشاخه نمیباشد.", 204);
    public static Error CanNotDelete = new("NotAccess", "به دلیل داشتن وابستگی اطلاعاتی نمیتوانین این گروه را حذف کنید.", 422);
    public static Error CanNotDeleteBecauseOfStandards = new("NotAccess", "به دلیل داشتن استاندارد مصرفی برای ماشین آلات و تجهیزات نمیتوانید این گروه را حذف کنید.", 422);
    public static Error CanNotDeleteBecauseOfVolumes = new("NotAccess", "به دلیل داشتن احجام مصرفی برای ماشین آلات و تجهیزات نمیتوانید این گروه را حذف کنید.", 422);
    public static Error CanNotForStatus = new("NotAccess", "امکان غیرفعال کردن گروه ماشین آلات به دلیل وجود ریزمتره درجریان وجود ندارد.", 422);
    public static Error IsDeleted = new("NotFound", "این گروه ماشین آلات و تجهیزات حذف شده است.", 204);

    public static Error UnValidId = new("InvalidArguments", "گروه ماشین آلات و تجهیزات با این شناسه نامعتبر است.", 422);
    public static Error UnValidData = new("InvalidArguments", "اطلاعات وارد شده نامعتبر است.", 422);
    public static Error IdIsEmpty = new("InvalidArguments", "شناسه خالی است!", 422);
    public static Error GroupNameIsEmpty = new("InvalidArguments", "نام خالی است!", 422);
    public static Error GroupCodeIsEmpty = new("InvalidArguments", "کد خالی است!", 422);
    public static Error IsActiveIsEmpty = new("InvalidArguments", "وضعیت خالی است!", 422);

    public static Error HaveChild = new("InvalidArguments", "گروه ماشین آلات و تجهیزات دارای وابستگی اطلاعاتی است و قابل تغییر نیست.", 422);
}