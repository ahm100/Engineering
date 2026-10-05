
namespace Engineering.Domain.Errors;

public static class OperationInfoServiceErrors
{
    public static Error NameIsDuplicate = new("Duplicate", "نام خدمت تکراری است.", 409);
    public static Error CodeIsDuplicate = new("Duplicate", "کد خدمت تکراری است.", 409);

    public static Error IsActive = new("InvalidArguments", "وضعیت خدمت فعال میباشد.", 422);
    public static Error IsInactive = new("InvalidArguments", "وضعیت خدمت غیرفعال میباشد.", 422);

    public static Error OperationInfoServiceWithIdNotFound = new("NotFound", "هیچ خدمت ای با این شناسه یافت نشد.", 404);
    public static Error OperationInfoServiceIsOk = new("NotFound", "خدمات درخواست شده در حال حاضر وجود دارند.", 404);
    public static Error ServiceIdsIsNotOk = new("NotFound", "شناسه های خدمات ارسالی صحیح نمیباشند.", 422);
    public static Error OperationInfoServiceWithCodeNotFound = new("NotFound", "هیچ خدمت ای با این شناسه یافت نشد.", 404);
    public static Error OperationInfoServiceWithNameNotFound = new("NotFound", "هیچ خدمتی با این نام یافت نشد.", 404);
    public static Error FilteredOperationInfoServiceNotFound = new("NotFound", "هیچ خدمت ای با این اطلاعات یافت نشد.", 204);
    public static Error OperationInfoServiceChildNotFound = new("NotFound", "خدمت انتخاب شده هیچگونه زیرشاخه ای ندارد.", 204);
    public static Error IsDeleted = new("NotFound", "این خدمت حذف شده است.", 204);

    public static Error UnValidId = new("InvalidArguments", "شناسه خدمت نامعتبر است.", 422);
    public static Error OperationInfoIsEmpty = new("InvalidArguments", "شناسه شرح عملیات خالی است.", 422);
    public static Error IdIsEmpty = new("InvalidArguments", "شناسه خالی است.", 422);

    public static Error OneIdMustSelect = new("InvalidArguments", "حداقل یک خدمت باید انتخاب شده باشد.", 204);
    public static Error NoHaveService = new("InvalidArguments", "شرح عملیات انتخابی هیچ خدمتی ندارد.", 204);

    public static Error HaveChild = new("InvalidArguments", "این خدمت بدلیل وابستگی اطلاعاتی قابل تغییر نیست.", 422);
}
