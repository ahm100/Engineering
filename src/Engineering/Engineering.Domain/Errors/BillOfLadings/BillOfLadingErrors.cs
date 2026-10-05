namespace Engineering.Domain.Errors;

public static class BillOfLadingErrors
{
    public static Error NameIsDuplicate = new("Duplicate", "نام بارنامه تکراری است.", 409);
    public static Error CodeIsDuplicate = new("Duplicate", "کد بارنامه تکراری است.", 409);
    public static Error InvalidCompany = new("Duplicate", "سازمان شما یافت نشد.", 409);

    public static Error IsActive = new("InvalidArguments", "وضعیت بارنامه فعال میباشد.", 422);
    public static Error IsInactive = new("InvalidArguments", "وضعیت بارنامه غیرفعال میباشد.", 422);
    public static Error CanNottDelete = new("InvalidArguments", "به دلیل داشتن درخواست ترابری، این بارنامه قابل حذف نمیباشد.", 422);

    public static Error IdIsEmpty = new("InvalidArguments", "شناسه بارنامه خالی است.", 422);
    public static Error NameIsEmpty = new("InvalidArguments", "نام بارنامه خالی است.", 422);
    public static Error CodeIsEmpty = new("InvalidArguments", "کد بارنامه خالی است.", 422);
    public static Error IsActiveIsEmpty = new("InvalidArguments", "وضعیت بارنامه خالی است.", 422);
    public static Error BillOfLadingWithIdNotFound = new("NotFound", "هیچ بارنامه ای با این شناسه یافت نشد.", 404);
    public static Error BillOfLadingWithCodeNotFound = new("NotFound", "هیچ بارنامه ای با این کد یافت نشد.", 204);
    public static Error BillOfLadingWithNameNotFound = new("NotFound", "هیچ بارنامه ای با این نام یافت نشد.", 204);
    public static Error FilteredBillOfLadingNotFound = new("NotFound", "هیچ بارنامه ای با این اطلاعات یافت نشد.", 204);
    public static Error BillOfLadingChildNotFound = new("NotFound", "بارنامه انتخابی هیچ زیرشاخه ای ندارد.", 204);
    public static Error IsDeleted = new("NotFound", "این بارنامه حذف شده است.", 204);

    public static Error UnValidId = new("InvalidArguments", "شناسه بارنامه نامعتبر است.", 422);
    public static Error UnValidData = new("InvalidArguments", "اطلاعات وارد شده نامعتبر است.", 422);

    public static Error HaveChild = new("InvalidArguments", "بارنامه انتخابی به دلیل داشتن درخواست ترابری، قابل تغییر نمیباشد.", 422);
    public static Error CanNottInActive = new("InvalidArguments", "بارنامه انتخابی به دلیل داشتن درخواست ترابری، قابل تغییر نمیباشد.", 422);
}