namespace Engineering.Domain.Errors.Messengers;

public static class MessengerErrors
{
    public static Error NotFound = new("InvalidArguments", "پیامرسانی با این شناسه پیدا نشد.", 204);
    public static Error NotFound404 = new("InvalidArguments", "پیامرسانی با این شناسه پیدا نشد.", 404);
    public static Error NoHistoryFound = new("InvalidArguments", "تاریخچه ی پیامی با این اطلاعات پیدا نشد.", 204);

    public static Error IdIsEmpty = new("InvalidArguments", "شناسه پیام رسان نامعتبر است.", 422);

}