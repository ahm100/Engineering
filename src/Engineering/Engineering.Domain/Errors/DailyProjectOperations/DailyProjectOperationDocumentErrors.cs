
namespace Engineering.Domain.Errors;

public static class DailyProjectOperationDocumentErrors
{
    public static Error DailyProjectOperationDocumentWithIdNotFound = new("NotFound", "مستندات کارکرد روزانه یافت نشد.", 404);

    public static Error InValidUrl = new("InvalidArguments", "لینک نامعتبر است.", 422);
    public static Error InValidDailyProjectOperation = new("InvalidArguments", "عملکرد روزانه نامعتبر است.", 422);
    public static Error InValidDailyProjectOperationDocumentId = new("InvalidArguments", "شناسه نامعتبر است.", 422);
    public static Error IsDeleted = new("NotFound", "مستندات کارکرد روزانه حذف شده است.", 204);
}
