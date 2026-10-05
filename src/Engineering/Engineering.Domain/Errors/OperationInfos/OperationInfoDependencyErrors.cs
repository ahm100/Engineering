
namespace Engineering.Domain.Errors;

public static class OperationInfoDependencyErrors
{
    public static Error NameIsDuplicate = new("Duplicate", "نام وابستگی تکراری است.", 409);
    public static Error CodeIsDuplicate = new("Duplicate", "کد وابستگی تکراری است.", 409);

    public static Error IsActive = new("InvalidArguments", "وضعیت وابستگی فعال میباشد.", 422);
    public static Error IsInactive = new("InvalidArguments", "وضعیت وابستگی غیرفعال میباشد.", 422);

    public static Error DependencyWithIdNotFound = new("NotFound", "هیچ وابستگی ای با این شناسه یافت نشد.", 404);
    public static Error DependencyWithCodeNotFound = new("NotFound", "هیچ وابستگی ای با این کد یافت نشد.", 404);
    public static Error DependencyWithNameNotFound = new("NotFound", "هیچ وابستگی ای با این نام یافت نشد.", 404);
    public static Error FilteredDependencyNotFound = new("NotFound", "هیچ وابستگی ای با این اطلاعات یافت نشد.", 204);
    public static Error DependencyChildNotFound = new("NotFound", "وابستگی انتخابی هیچ زیرشاخه ای ندارد.", 204);
    public static Error IsDeleted = new("NotFound", "این وابستگی حذف شده است.", 204);

    public static Error IdIsEmpty = new("InvalidArguments", "شناسه خالی است.", 422);
    public static Error OperationInfoIdIsEmpty = new("InvalidArguments", "شناسه شرح عملیات خالی است.", 422);
    public static Error UnValidId = new("InvalidArguments", "اطلاعات وارد شده نامعتبر است.", 422);
    public static Error InValidDependencyType = new("InvalidArguments", "نوع وابستگی نامعتبر است.", 422);
    public static Error RelationIdIsEmpty = new("InvalidArguments", "شناسه وابستگی خالی است.", 422);
    public static Error PriorityIsEmpty = new("InvalidArguments", "الویت وابستگی خالی است.", 422);
    public static Error DependencyTypeIsEmpty = new("InvalidArguments", "نوع وابستگی خالی است.", 422);
    public static Error OperationInfoIsEmpty = new("InvalidArguments", "شرح عملیات خالی است.", 422);
    public static Error WorkingDaysIsEmpty = new("InvalidArguments", "روز کاری خالی است!", 422);

    public static Error HaveChild = new("InvalidArguments", "وابستگی انتخابی به دلیل وابستگی اطلاعاتی، قابل تغییر نمیباشد.", 422);
}