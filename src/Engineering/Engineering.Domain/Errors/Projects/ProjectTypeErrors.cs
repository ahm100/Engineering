
namespace Engineering.Domain.Errors;

public static class ProjectTypeErrors
{
    public static Error TypeNameIsDuplicate = new("Duplicate", "نام نوع پروژه تکراری است.", 409);
    public static Error TypeCodeIsDuplicate = new("Duplicate", "کد نوع پروژه تکراری است.", 409);

    public static Error IdIsEmpty = new("InvalidArguments", "شناسه خالی است.", 422);
    public static Error ProjectTypeCodeIsEmpty = new("InvalidArguments", "کد خالی است.", 422);
    public static Error ProjectTypeNameIsEmpty = new("InvalidArguments", "نام خالی است.", 422);
    public static Error IsActiveIsEmpty = new("InvalidArguments", "وضعیت خالی است.", 422);
    public static Error ProjectTypeWithIdNotFound = new("NotFound", "هیچ نوع پروژه ای با این شناسه یافت نشد.", 404);
    public static Error ProjectTypeWithCodeNotFound = new("NotFound", "هیچ نوع پروژه ای با این کد یافت نشد.", 404);
    public static Error ProjectTypeWithNameNotFound = new("NotFound", "هیچ نوع پروژه ای با این عنوان یافت نشد.", 404);
    public static Error ProjectTypesNotFound = new("NotFound", "هیچ نوع پروژه ای با این اطلاعات یافت نشد.", 404);
    public static Error IsInactive = new("InvalidArguments", "وضعیت نوع پروژه غیرفعال میباشد.", 422);
    public static Error IsActive = new("InvalidArguments", "وضعیت نوع پروژه فعال میباشد.", 422);
    public static Error IsDeleted = new("NotFound", "این نوع پروژه حذف شده است.", 404);
}