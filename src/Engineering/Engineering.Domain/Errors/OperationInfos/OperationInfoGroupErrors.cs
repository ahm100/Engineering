
namespace Engineering.Domain.Errors;

public static class OperationInfoGroupErrors
{
    public static Error GroupNameIsDuplicate = new("Duplicate", "نام گروه شرح عملیات تکراری است.", 409);
    public static Error GroupCodeIsDuplicate = new("Duplicate", "کد گروه شرح عملیات تکراری است.", 409);

    public static Error IdIsEmpty = new("InvalidArguments", "شناسه خالی است.", 422);
    public static Error OperationInfoGroupCodeIsEmpty = new("InvalidArguments", "کد خالی است.", 422);
    public static Error OperationInfoGroupNameIsEmpty = new("InvalidArguments", "نام خالی است.", 422);
    public static Error IsActiveIsEmpty = new("InvalidArguments", "وضعیت خالی است.", 422);
    public static Error OperationInfoGroupWithIdNotFound = new("NotFound", "هیچ گروه شرح عملیات ای با این شناسه یافت نشد.", 404);
    public static Error OperationInfoGroupWithCodeNotFound = new("NotFound", "هیچ گروه شرح عملیات ای با این کد یافت نشد.", 404);
    public static Error OperationInfoGroupWithNameNotFound = new("NotFound", "هیچ گروه شرح عملیات ای با این عنوان یافت نشد.", 404);
    public static Error OperationInfoGroupsNotFound = new("NotFound", "هیچ گروه شرح عملیات ای با این اطلاعات یافت نشد.", 404);
    public static Error IsInactive = new("InvalidArguments", "وضعیت گروه شرح عملیات غیرفعال میباشد.", 422);
    public static Error IsActive = new("InvalidArguments", "وضعیت گروه شرح عملیات فعال میباشد.", 422);
    public static Error IsDeleted = new("NotFound", "این گروه شرح عملیات حذف شده است.", 404);
}