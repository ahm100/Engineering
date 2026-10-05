
namespace Engineering.Domain.Errors;

public static class DailyProjectOperationExpertErrors
{
    public static Error DailyProjectOperationExpertWithIdNotFound = new("NotFound", "متخصص عملکرد روزانه یافت نشد.", 404);

    public static Error InValidThirdPartyId = new("InvalidArguments", "متخصص نامعتبر است.", 422);
    public static Error InValidFinalValue = new("InvalidArguments", "مقدار نامعتبر است.", 422);
    public static Error InValidDailyProjectOperation = new("InvalidArguments", "عملکرد روزانه نامعتبر است.", 422);
    public static Error InValidConsumableVolumeExpert = new("InvalidArguments", "منابع انسانی ریز متره نامعتبر است.", 422);
    public static Error IsDeleted = new("NotFound", "متخصص عملکرد روزانه حذف شده است.", 204);
}
