
namespace Engineering.Domain.Errors;

public static class ProjectOperationDependencyErrors
{
    public static Error NameIsDuplicate = new("Duplicate", "نام وابستگی تکراری است.", 409);
    public static Error CodeIsDuplicate = new("Duplicate", "کد وابستگی تکراری است.", 409);

    public static Error IsActive = new("InvalidArguments", "وضعیت وابستگی فعال میباشد.", 422);
    public static Error IsInactive = new("InvalidArguments", "وضعیت وابستگی غیرفعال میباشد.", 422);

    public static Error DependencyWithIdNotFound = new("NotFound", "هیچ وابستگی ای با این شناسه یافت نشد.", 404);
    public static Error ProjectOperationDependencyWithIdNotFound = new("NotFound", "هیچ وابستگی ای با این شناسه یافت نشد.", 404);
    public static Error DependencyWithCodeNotFound = new("NotFound", "هیچ وابستگی ای با این کد یافت نشد.", 404);
    public static Error DependencyWithNameNotFound = new("NotFound", "هیچ وابستگی ای با این نام یافت نشد.", 404);
    public static Error FilteredDependencyNotFound = new("NotFound", "هیچ وابستگی ای با این اطلاعات یافت نشد.", 204);
    public static Error DependencyChildNotFound = new("NotFound", "وابستگی انتخابی هیچ زیرشاخه ای ندارد.", 204);
    public static Error IsDeleted = new("NotFound", "این وابستگی شرح عملیات حذف شده است.", 204);
    public static Error NoChanges = new("NoChange", "دیتایی تغییر نکرد برای تغییر لطفا پارامتر های ورودی را بررسی کنید.", 200);

    public static Error ProjectOperationIsEmpty = new("InvalidArguments", "شرح عملیات پروژه خالی است.", 422);
    public static Error ProjectOperationIdIsEmpty = new("InvalidArguments", "شناسه شرح عملیات پروژه خالی است.", 422);
    public static Error RelationIdIsEmpty = new("InvalidArguments", "شناسه وابستگی خالی است.", 422);
    public static Error RelationDaysIsEmpty = new("InvalidArguments", "تعداد روز وابستگی خالی است.", 422);
    public static Error DependencyTypeIsEmpty = new("InvalidArguments", "نوع وابستگی خالی است.", 422);
    public static Error UnValidId = new("InvalidArguments", "اطلاعات وارد شده نامعتبر است.", 422);
    public static Error IdIsEmpty = new("InvalidArguments", "شناسه خالی است.", 422);

    public static Error HaveChild = new("InvalidArguments", "وابستگی انتخابی به دلیل وابستگی اطلاعاتی، قابل تغییر نمیباشد.", 422);

    public static Error HaveFSDependency(string operationName) =>
    new(
        "HaveFSDependency",
        $"به دلیل وابستگی شروع به پایان به شرح عملیات {operationName} قابل اعمال تغییرات نیست.",
        422);

    public static Error HaveSSDependency(string operationName) =>
    new(
        "HaveFSDependency",
        $"به دلیل وابستگی شروع به شروع به شرح عملیات {operationName} قابل اعمال تغییرات نیست.",
        422);

    public static Error HaveFFDependency(string operationName) =>
    new(
        "HaveFSDependency",
        $"به دلیل وابستگی پایان به پایان به شرح عملیات {operationName} قابل اعمال تغییرات نیست.",
        422);

    public static Error HaveSFDependency(string operationName) =>
    new(
        "HaveFSDependency",
        $"به دلیل وابستگی پایان به شروع به شرح عملیات {operationName} قابل اعمال تغییرات نیست.",
        422);
}
