
namespace Engineering.Domain.Errors;

public static class OperationInfoGroupRelationErrors
{
    public static Error NameIsDuplicate = new("Duplicate", "نام گروه تکراری است.", 409);
    public static Error CodeIsDuplicate = new("Duplicate", "کد گروه تکراری است.", 409);

    public static Error IsActive = new("InvalidArguments", "وضعیت گروه فعال میباشد.", 422);
    public static Error IsInactive = new("InvalidArguments", "وضعیت گروه غیرفعال میباشد.", 422);

    public static Error OperationInfoGroupRelationWithIdNotFound = new("NotFound", "هیچ گروه ای با این شناسه یافت نشد.", 404);
    public static Error OperationInfoGroupRelationIsOk = new("NotFound", "گروه درخواست شده در حال حاضر وجود دارند.", 404);
    public static Error OperationInfoGroupRelationIsNotOk = new("NotFound", "گروه درخواست شده در حال حاضر وجود دارند.", 404);
    public static Error GroupIdsIsNotOk = new("NotFound", "شناسه های گروه ارسالی صحیح نمیباشند.", 422);
    public static Error OperationInfoGroupRelationWithCodeNotFound = new("NotFound", "هیچ گروه ای با این شناسه یافت نشد.", 404);
    public static Error OperationInfoGroupRelationWithNameNotFound = new("NotFound", "هیچ گروهی با این نام یافت نشد.", 404);
    public static Error FilteredOperationInfoGroupRelationNotFound = new("NotFound", "هیچ گروه ای با این اطلاعات یافت نشد.", 204);
    public static Error OperationInfoGroupRelationChildNotFound = new("NotFound", "گروه انتخاب شده هیچگونه زیرشاخه ای ندارد.", 204);
    public static Error IsDeleted = new("NotFound", "این گروه حذف شده است.", 204);

    public static Error UnValidId = new("InvalidArguments", "شناسه گروه نامعتبر است.", 422);
    public static Error UnValidGroups = new("InvalidArguments", "شناسه گروه نامعتبر است.", 422);
    public static Error OperationInfoIsEmpty = new("InvalidArguments", "شناسه شرح عملیات خالی است.", 422);
    public static Error IdIsEmpty = new("InvalidArguments", "شناسه خالی است.", 422);

    public static Error OneIdMustSelect = new("InvalidArguments", "حداقل یک گروه باید انتخاب شده باشد.", 422);
    public static Error NoHaveGroup = new("InvalidArguments", "شرح عملیات انتخابی هیچ گروهی ندارد.", 422);

    public static Error HaveChild = new("InvalidArguments", "این گروه بدلیل وابستگی اطلاعاتی قابل تغییر نیست.", 422);
}