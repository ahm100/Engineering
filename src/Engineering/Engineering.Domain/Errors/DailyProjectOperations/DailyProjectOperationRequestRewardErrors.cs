
namespace Engineering.Domain.Errors;

public static class DailyProjectOperationRequestRewardErrors
{
    public static Error DailyProjectOperationRequestRewardWithIdNotFound = new("NotFound", "متخصص عملکرد روزانه یافت نشد.", 404);

    public static Error InValidDailyProjectOperation = new("InvalidArguments", "عملکرد روزانه نامعتبر است.", 422);
    public static Error InValidRequestRewardId = new("InvalidArguments", "شناسه پاداش جریمه نامعتبر است.", 422);
    public static Error IsDeleted = new("NotFound", "متخصص عملکرد روزانه حذف شده است.", 204);
}
