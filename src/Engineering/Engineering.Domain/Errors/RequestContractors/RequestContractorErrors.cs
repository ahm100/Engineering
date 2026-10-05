
namespace Engineering.Domain.Errors;

public static class RequestContractorErrors
{
    public static Error RequestContractorNotFound = new("NotFound", "درخواست پیمانکار یافت نشد.", 404);
    public static Error RequestContractorsNotFound = new("NotFound", "درخواست های پیمانکار یافت نشد.", 204);

    public static Error UnvalidStatus = new("InvalidArguments", "وضعیت درخواست نامعتبر است.", 422);
    public static Error InValidVolume = new("InvalidArguments", "حجم نامعتبر است.", 422);
    public static Error InValidProjectOperationDetail = new("InvalidArguments", "ریزمتره نامعتبر است.", 422);
    public static Error InValidProjectOperationDetailModel = new("InvalidArguments", "مدل ریزمتره نامعتبر است.", 422);
    public static Error InValidServiceInfoModel = new("InvalidArguments", "مدل خدمات نامعتبر است.", 422);
    public static Error InValidServiceInfo = new("InvalidArguments", "خدمات نامعتبر است.", 422);
    public static Error InValidRequestContractor = new("InvalidArguments", "درخواست پیمانکار نامعتبر است.", 422);
    public static Error InValidId = new("InvalidArguments", "شناسه درخواست پیمانکار نامعتبر است.", 422);
    public static Error InValidIds = new("InvalidArguments", "شناسه های درخواست پیمانکار نامعتبر است.", 422);
    public static Error IsDeleted = new("InvalidArguments", "درخواست پیمانکار مد نظر حذف شده است.", 422);
    public static Error UnvalidData = new("InvalidArguments", "برای ایجاد درخواست پیمانکار ریزمتره یا خدمات نامعتبر وارد شده است.", 422);
    public static Error CurrenciesNotFound = new("InvalidArguments", "موردی از ارز های ارسالی نامعتبر میباشند.", 422);
    public static Error ContractorsNotFound = new("InvalidArguments", "موردی از پیمانکاران ارسالی نامعتبر میباشند.", 422);
    public static Error IsDuplicate(string? publicName, string? serviceName) => new("InvalidArguments", $"برای ریزمتره {publicName} و خدمت {serviceName} درخواست وجود دارد.", 422);
}
