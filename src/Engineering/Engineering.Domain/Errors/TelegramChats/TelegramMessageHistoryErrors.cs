namespace Engineering.Domain.Errors;

public static class TelegramMessageHistoryErrors
{
    public static Error UnValidId = new("InvalidArguments", "شناسه پیام ارسال نشده نامعتبر است.", 422);
    public static Error UnValidTelegramChatId = new("InvalidArguments", "شناسه چت تلگرام نامعتبر است.", 422);
    public static Error UnValidTelegramMessageType = new("InvalidArguments", "تایپ چت نامعتبر است.", 422);
    public static Error UnValidMessage = new("InvalidArguments", "متن پیام نامعتبر است.", 422);

    public static Error IsDeleted = new("NotFound", "این پیام حذف شده است.", 204);
    public static Error FilteredTelegramMessageHistoryNotFound = new("NotFound", "هیچ پیامی با این اطلاعات یافت نشد.", 204);
}