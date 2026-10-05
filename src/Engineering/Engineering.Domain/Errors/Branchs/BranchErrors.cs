namespace Engineering.Domain.Errors;

public static class BranchErrors
{
    public static Error NameIsDuplicate = new("Duplicate", "نام رشته تکراری است.", 409);
    public static Error CodeIsDuplicate = new("Duplicate", "کد رشته تکراری است.", 409);

    public static Error IsActive = new("InvalidArguments", "وضعیت رشته فعال میباشد.", 422);
    public static Error IsInactive = new("InvalidArguments", "وضعیت رشته غیرفعال میباشد.", 422);
    public static Error CanNottDelete = new("InvalidArguments", " به دلیل داشتن شرح عملیات برای گروه، این رشته قابل حذف نمیباشد.", 422);

    public static Error BranchCategoryNotFound = new("NotFound", "رسته انتخابی یافت نشد.", 404);
    public static Error BranchCategoryNotFoundWithCode = new("NotFound", "برای رشته شما رسته ای یافت نشد.", 404);
    public static Error BranchWithIdNotFound = new("NotFound", "هیچ رشته ای با این شناسه یافت نشد.", 404);
    public static Error BranchWithCodeNotFound = new("NotFound", "هیچ رشته ای با این کد یافت نشد.", 204);
    public static Error BranchWithNameNotFound = new("NotFound", "هیچ رشته ای با این نام یافت نشد.", 204);
    public static Error FilteredBranchNotFound = new("NotFound", "هیچ رشته ای با این اطلاعات یافت نشد.", 204);
    public static Error BranchChildNotFound = new("NotFound", "رشته انتخابی هیچ زیرشاخه ای ندارد.", 204);
    public static Error BranchNotFound = new("NotFound", "رشته انتخابی پیدا نشد.", 204);
    public static Error IsDeleted = new("NotFound", "این رشته حذف شده است.", 204);

    public static Error UnValidId = new("InvalidArguments", "شناسه رشته نامعتبر است.", 422);
    public static Error UnValidData = new("InvalidArguments", "اطلاعات وارد شده نامعتبر است.", 422);

    public static Error IdIsEmpty = new("InvalidArguments", "شناسه رشته خالی است.", 422);
    public static Error CategoryIdIsEmpty = new("InvalidArguments", "شناسه رسته خالی است.", 422);
    public static Error BranchNameIsEmpty = new("InvalidArguments", "نام رسته خالی است.", 422);
    public static Error BranchCodeIsEmpty = new("InvalidArguments", "کد رشته خالی است.", 422);
    public static Error IsActiveIsEmpty = new("InvalidArguments", "وضعیت رشته خالی است.", 422);

    public static Error HaveChild = new("InvalidArguments", "رشته انتخابی به دلیل وابستگی اطلاعاتی، قابل تغییر نمیباشد.", 422);
}