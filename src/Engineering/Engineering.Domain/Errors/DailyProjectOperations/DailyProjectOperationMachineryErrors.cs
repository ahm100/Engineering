
namespace Engineering.Domain.Errors;

public static class DailyProjectOperationMachineryErrors
{
    public static Error DailyProjectOperationMachineryWithIdNotFound = new("NotFound", "ماشین آلات و تجهیزات عملکرد روزانه یافت نشد.", 404);

    public static Error InValidMachineId = new("InvalidArguments", "ماشین آلات و تجهیزات نامعتبر است.", 422);
    public static Error InValidFinalValue = new("InvalidArguments", "مقدار نامعتبر است.", 422);
    public static Error InValidDailyProjectOperation = new("InvalidArguments", "عملکرد روزانه نامعتبر است.", 422);
    public static Error InValidConsumableVolumeMachinery = new("InvalidArguments", "ماشین آلات ریز متر نامعتبر است.", 422);
    public static Error IsDeleted = new("NotFound", "ماشین آلات و تجهیزات عملکرد روزانه حذف شده است.", 204);
}
