
namespace Engineering.Domain.Errors;

public static class RequestRewardThirdPartyErrors
{
    public static Error RequestRewardThirdPartyNotFound = new("NotFound", "طرف حساب یافت نشد.", 404);

    public static Error InValidRequestReward = new("InvalidArguments", "پاداش و جریمه نا معتبر است.", 422);
    public static Error InValidThirdParty = new("InvalidArguments", "شخص نا معتبر است.", 422);
}
