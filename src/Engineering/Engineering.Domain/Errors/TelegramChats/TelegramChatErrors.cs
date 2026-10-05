
namespace Engineering.Domain.Errors;

public static class TelegramChatErrors
{
    public static Error NameIsDuplicate = new("Duplicate", "نام گروه تلگرام تکراری است.", 409);
    public static Error CodeIsDuplicate = new("Duplicate", "کد گروه تلگرام تکراری است.", 409);

    public static Error MessageNotSend = new("InvalidArguments", "ارسال پیام تلگرام با خطا مواجه شد.", 422);
    public static Error IsActive = new("InvalidArguments", "وضعیت گروه تلگرام فعال میباشد.", 422);
    public static Error IsInactive = new("InvalidArguments", "وضعیت گروه تلگرام غیرفعال میباشد.", 422);
    public static Error CanNottDelete = new("InvalidArguments", " به دلیل داشتن شرح عملیات برای گروه، این گروه تلگرام قابل حذف نمیباشد.", 422);
    public static Error InValidChatId = new("InvalidArguments", "چت آیدی نامعتبر است", 422);

    public static Error TelegramChatProjectIdNotFound = new("NotFound", "پروژه انتخابی یافت نشد.", 404);
    public static Error TelegramChatCostCenterNotFound = new("NotFound", "مرکز هزینه انتخابی یافت نشد.", 404);
    public static Error TelegramChatCategoryNotFoundWithCode = new("NotFound", "برای گروه تلگرام شما رسته ای یافت نشد.", 404);
    public static Error TelegramChatWithIdNotFound = new("NotFound", "هیچ گروه تلگرام ای با این شناسه یافت نشد.", 404);
    public static Error TelegramChatWithCodeNotFound = new("NotFound", "هیچ گروه تلگرام ای با این کد یافت نشد.", 204);
    public static Error TelegramChatWithNameNotFound = new("NotFound", "هیچ گروه تلگرام ای با این نام یافت نشد.", 204);
    public static Error FilteredTelegramChatNotFound = new("NotFound", "هیچ گروه تلگرام ای با این اطلاعات یافت نشد.", 204);
    public static Error TelegramChatChildNotFound = new("NotFound", "گروه تلگرام انتخابی هیچ زیرشاخه ای ندارد.", 204);
    public static Error IsDeleted = new("NotFound", "این گروه تلگرام حذف شده است.", 204);

    public static Error UnValidId = new("InvalidArguments", "شناسه گروه تلگرام نامعتبر است.", 422);
    public static Error UnValidData = new("InvalidArguments", "اطلاعات وارد شده نامعتبر است.", 422);

    public static Error IdIsEmpty = new("InvalidArguments", "شناسه گروه تلگرام خالی است.", 422);
    public static Error CostCenterIdIsEmpty = new("InvalidArguments", "شناسه مرکز هزینه خالی است.", 422);
    public static Error ChatIdIsEmpty = new("InvalidArguments", "شناسه چت خالی است.", 422);
    public static Error TelegramChatNameIsEmpty = new("InvalidArguments", "نام گروه تلگرام خالی است.", 422);
    public static Error TelegramChatUrlIsEmpty = new("InvalidArguments", "آدرس گروه تلگرام خالی است.", 422);
    public static Error IsActiveIsEmpty = new("InvalidArguments", "وضعیت گروه تلگرام خالی است.", 422);

    public static Error HaveChild = new("InvalidArguments", "گروه تلگرام انتخابی به دلیل وابستگی اطلاعاتی، قابل تغییر نمیباشد.", 422);

}