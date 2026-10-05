namespace Engineering.Domain.Errors.Actions;

public class ActionErrors
{
    public static Error NameIsDuplicate = new("Duplicate", "نام عملیات تکراری است.", 409);
    public static Error CodeIsDuplicate = new("Duplicate", "کد عملیات تکراری است.", 409);
    public static Error InvalidCompany = new("Duplicate", "سازمان شما یافت نشد.", 409);

    public static Error IsActive = new("InvalidArguments", "وضعیت عملیات فعال میباشد.", 422);
    public static Error IsInactive = new("InvalidArguments", "وضعیت عملیات غیرفعال میباشد.", 422);
    public static Error IdIsEmpty = new("InvalidArguments", "شناسه عملیات خالی است.", 422);
    public static Error PriceShouldBePositive = new("InvalidArguments", "هزینه نمیتواند از 0 کمتر باشد.", 422);
    public static Error NameIsEmpty = new("InvalidArguments", "نام عملیات خالی است.", 422);
    public static Error CodeIsEmpty = new("InvalidArguments", "کد عملیات خالی است.", 422);


    public static Error ActionWithIdNotFound = new("NotFound", "هیچ عملیات با این شناسه یافت نشد.", 404);
    public static Error ActionWithCodeNotFound = new("NotFound", "هیچ عملیات با این کد یافت نشد.", 204);
    public static Error ActionWithNameNotFound = new("NotFound", "هیچ عملیات با این نام یافت نشد.", 204);
    public static Error FilteredActionNotFound = new("NotFound", "هیچ عملیات با این اطلاعات یافت نشد.", 204);
    public static Error ActionChildNotFound = new("NotFound", "عملیات انتخابی هیچ زیرشاخه ای ندارد.", 204);
    public static Error IsDeleted = new("NotFound", "این عملیات حذف شده است.", 204);

    public static Error UnValidId = new("InvalidArguments", "شناسه عملیات نامعتبر است.", 422);
    public static Error UnValidData = new("InvalidArguments", "اطلاعات وارد شده نامعتبر است.", 422);
}
