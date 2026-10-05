namespace Engineering.Domain.Errors;

public static class CategoryErrors
{
    public static Error NameIsDuplicate = new("Duplicate", "نام رسته تکراری است.", 409);
    public static Error CodeIsDuplicate = new("Duplicate", "کد رسته تکراری است.", 409);

    public static Error IsActive = new("InvalidArguments", "وضعیت رسته فعال میباشد.", 422);
    public static Error IsInactive = new("InvalidArguments", "وضعیت رسته غیرفعال میباشد.", 422);
    public static Error CanNottDelete = new("InvalidArguments", " به دلیل داشتن شرح عملیات برای گروه های رشته این رسته قابل حذف نمیباشد.", 422);
    public static Error CanNottDeletebecauseOfOpInfo = new("InvalidArguments", " به دلیل داشتن شرح عملیات برای گروه های رشته، این رسته قابل حذف نمیباشد.", 422);
    public static Error CanNottDeletebecauseOfProjects = new("InvalidArguments", "به دلیل داشتن پروژه، این رسته قابل حذف نمیباشد.", 422);

    public static Error CategoryWithIdNotFound = new("NotFound", "هیچ رسته ای با این شناسه یافت نشد.", 404);
    public static Error CategoryWithCodeNotFound = new("NotFound", "هیچ رسته ای با این کد یافت نشد.", 204);
    public static Error CategoryWithNameNotFound = new("NotFound", "هیچ رسته ای با این نام یافت نشد.", 204);
    public static Error NotFound = new("NotFound", "هیچ رسته ای با این نام یافت نشد.", 404);
    public static Error FilteredCategoryNotFound = new("NotFound", "اطلاعاتی با فیلتر های ورودی یافت نشد.", 204);
    public static Error CategoryChildNotFound = new("NotFound", "رسته انتخابی هیچ زیرشاخه ای ندارد.", 204);
    public static Error IsDeleted = new("NotFound", "این رسته حذف شده است.", 404);

    public static Error HaveChild = new("InvalidArguments", "رسته انتخابی به دلیل وابستگی اطلاعاتی، قابل تغییر نمیباشد.", 422);

    public static Error UnValidId = new("InvalidArguments", "اطلاعات وارد شده نامعتبر است.", 422);
    public static Error IdIsEmpty = new("InvalidArguments", "شناسه رسته خالی است!", 422);
    public static Error CategoryCodeIsEmpty = new("InvalidArguments", "کد رسته خالی است!", 422);
    public static Error CategoryNameIsEmpty = new("InvalidArguments", "نام رسته خالی است!", 422);
    public static Error IsActiveIsEmpty = new("InvalidArguments", "وضعیت رسته خالی است!", 422);
    public static Error FilterDataIsEmpty = new("InvalidArguments", "دیتای فیلتر خالی است!", 422);
}