
namespace Engineering.Domain.Errors;

public static class RequestRewardProductErrors
{
    public static Error RequestRewardProductNotFound = new("NotFound", "محصول جریمه پاداش یافت نشد.", 404);

    public static Error InValidProduct = new("InvalidArguments", "محصول معتبر نیست.", 422);
    public static Error InValidPrice = new("InvalidArguments", "قیمت نامعتبر است.", 422);
    public static Error InValidCount = new("InvalidArguments", "تعداد نامعتبر است.", 422);
    public static Error InValidCurrency = new("InvalidArguments", "ارز نامعتبر است.", 422);
    public static Error InValidRequestReward = new("InvalidArguments", "جریمه پاداش معتبر نیست.", 422);
}
