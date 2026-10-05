
namespace Engineering.Domain.Errors;

public static class SeasonErrors
{
    public static Error NameIsDuplicate = new("Duplicate", "نام فصل تکراری است.", 409);
    public static Error CodeIsDuplicate = new("Duplicate", "کد فصل تکراری است.", 409);
    public static Error CategoryCodeIsDuplicate = new("Duplicate", "کد رسته تکراری است.", 409);

    public static Error IsActive = new("InvalidArguments", "وضعیت فصل فعال میباشد.", 422);
    public static Error IsInactive = new("InvalidArguments", "وضعیت فصل غیرفعال میباشد.", 422);
    public static Error CanNottDelete = new("InvalidArguments", "این فصل به دلیل داشتن وابستگی اطلاعاتی قابل حذف نمیباشد.", 422);
    public static Error CanNottDeleteForOperationInfo = new("InvalidArguments", " به دلیل داشتن شرح عملیات این فصل قابل حذف نمیباشد.", 422);

    public static Error SeasonBranchNotFound = new("NotFound", "رشته انتخابی یافت نشد.", 204);
    public static Error SeasonNotFound = new("NotFound", "فصل یافت نشد.", 204);
    public static Error SeasonWithIdNotFound = new("NotFound", "هیچ فصلی با این شناسه یافت نشد.", 404);
    public static Error SeasonBranchNotFoundWithCode = new("NotFound", "برای فصل شما رشته ای یافت نشد.", 404);
    public static Error SeasonWithCodeNotFound = new("NotFound", "هیچ فصلی با این کد یافت نشد.", 404);
    public static Error SeasonWithNameNotFound = new("NotFound", "هیچ فصلی با این نام یافت نشد.", 404);
    public static Error IsDeleted = new("NotFound", "این فصل حذف شده است.", 204);
    public static Error FilteredSeasonNotFound = new("NotFound", "هیچ فصلی با این اطلاعات یافت نشد.", 204);
    public static Error BranchChildNotFound = new("NotFound", "رشته انتخابی هیچ زیرشاخه ای ندارد.", 204);

    public static Error UnValidId = new("InvalidArguments", "شناسه فصل نامعتبر است.", 422);
    public static Error IdIsEmpty = new("InvalidArguments", "شناسه خالی است.", 422);
    public static Error SeasonNameIsEmpty = new("InvalidArguments", "نام خالی است.", 422);
    public static Error SeasonCodeIsEmpty = new("InvalidArguments", "کد خالی است.", 422);
    public static Error IsActiveIsEmpty = new("InvalidArguments", "وضعیت خالی است.", 422);
    public static Error BranchIsEmpty = new("InvalidArguments", "رشته خالی است.", 422);

    public static Error InvalidUnitNames(string? UnitNames) => new Error("ValidationData", $"واحد های {UnitNames}  ایجاد نشده اند.", 400);
    public static Error InvalidSeasonNames(string? SeasonNames) => new Error("ValidationData", $"فصل های {SeasonNames}  ایجاد نشده اند.", 400);
    public static Error InvalidBranchNames(string? BranchNames) => new Error("ValidationData", $"رشته های {BranchNames}  ایجاد نشده اند.", 400);
    public static Error InvalidCategoryNames(string? CategoryNames) => new Error("ValidationData", $"رسته های {CategoryNames}  ایجاد نشده اند.", 400);
}