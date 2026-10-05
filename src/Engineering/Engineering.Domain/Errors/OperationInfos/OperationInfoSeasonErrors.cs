
namespace Engineering.Domain.Errors;

public static class OperationInfoSeasonErrors
{
    public static Error NameIsDuplicate = new("Duplicate", "نام گروه تکراری است.", 409);
    public static Error CodeIsDuplicate = new("Duplicate", "کد گروه تکراری است.", 409);

    public static Error IsActive = new("InvalidArguments", "وضعیت گروه فعال میباشد.", 422);
    public static Error IsInactive = new("InvalidArguments", "وضعیت گروه غیرفعال میباشد.", 422);

    public static Error OperationInfoSeasonWithIdNotFound = new("NotFound", "هیچ فصل(گروه) ای با این شناسه شرح عملیات پروژه وجود ندارد.", 404);
    public static Error OperationInfoSeasonIsOk = new("NotFound", "خدمات درخواست شده در حال حاضر وجود دارند.", 404);
    public static Error SeasonIdsIsNotOk = new("NotFound", "شناسه های خدمات ارسالی صحیح نمیباشند.", 422);
    public static Error OperationInfoSeasonWithCodeNotFound = new("NotFound", "هیچ گروه ای با این شناسه یافت نشد.", 404);
    public static Error OperationInfoSeasonWithNameNotFound = new("NotFound", "هیچ گروهی با این نام یافت نشد.", 404);
    public static Error FilteredOperationInfoSeasonNotFound = new("NotFound", "هیچ گروه ای با این اطلاعات یافت نشد.", 204);
    public static Error OperationInfoSeasonChildNotFound = new("NotFound", "گروه انتخاب شده هیچگونه زیرشاخه ای ندارد.", 204);
    public static Error HaveRequestGoodsSupply = new("NotFound", "این گروه دارای درخواست تامین میباشد و شما نمیتوانین حذفش کنین", 404);
    public static Error IsDeleted = new("NotFound", "این گروه حذف شده است.", 204);

    public static Error UnValidId = new("InvalidArguments", "شناسه گروه نامعتبر است.", 422);
    public static Error UnValidSeasons = new("InvalidArguments", "شناسه گروه نامعتبر است.", 422);
    public static Error OperationInfoIsEmpty = new("InvalidArguments", "شناسه شرح عملیات خالی است.", 422);
    public static Error IdIsEmpty = new("InvalidArguments", "شناسه خالی است.", 422);

    public static Error OneIdMustSelect = new("InvalidArguments", "حداقل یک گروه باید انتخاب شده باشد.", 422);
    public static Error NoHaveSeason = new("InvalidArguments", "شرح عملیات انتخابی هیچ گروهی ندارد.", 422);

    public static Error HaveChild = new("InvalidArguments", "این گروه بدلیل وابستگی اطلاعاتی قابل تغییر نیست.", 422);

    public static Error OINotAssignedToSeason = new("InvalidArguments", "فصل به شرح عملیات پروژه تخصیص داده نشده است.", 422);
}