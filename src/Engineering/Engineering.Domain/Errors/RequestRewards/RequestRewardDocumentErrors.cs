
namespace Engineering.Domain.Errors;

public static class RequestRewardDocumentErrors
{
    public static Error RequestRewardDocumentNotFound = new("NotFound", "پیوست یافت نشد.", 404);

    public static Error InValidRequestReward = new("InvalidArguments", "پاداش و جریمه نا معتبر است.", 422);
    public static Error InValidUrl = new("InvalidArguments", "لینک نا معتبر است.", 422);
    public static Error InValidId = new("InvalidArguments", "شناسه نا معتبر است.", 422);
}
