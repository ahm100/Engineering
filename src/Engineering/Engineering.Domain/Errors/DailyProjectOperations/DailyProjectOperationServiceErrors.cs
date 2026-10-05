
namespace Engineering.Domain.Errors;

public static class DailyProjectOperationServiceErrors
{
    public static Error DailyProjectOperationServiceWithIdNotFound = new("NotFound", "خدمت عملکرد روزانه یافت نشد.", 404);

    public static Error InValidDailyProjectOperation = new("InvalidArguments", "عملکرد روزانه نامعتبر است.", 422);
    public static Error InValidContractorService = new("InvalidArguments", "خدمت ریز متر نامعتبر است.", 422);
    public static Error IsDeleted = new("NotFound", "خدمت عملکرد روزانه حذف شده است.", 204);
}
