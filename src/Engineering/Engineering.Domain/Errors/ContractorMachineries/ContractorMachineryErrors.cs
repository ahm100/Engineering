
namespace Engineering.Domain.Errors;

public static class ContractorMachineryErrors
{
    public static Error ContractorMachineryIsDuplicate = new("Duplicate", "ماشین آلات پیمانکار تکراری است.", 409);

    public static Error ContractorMachineryNotFoundWithId = new("NotFound", "ماشین آلات پیمانکار با این شناسه یافت نشد.", 404);

    public static Error IdIsEmpty = new("InvalidArguments", "شناسه خالی است.", 422);
    public static Error ContractorIdIsEmpty = new("InvalidArguments", "شناسه پیمانکار خالی است.", 422);
    public static Error CurrencyIdIsEmpty = new("InvalidArguments", "شناسه واحد ارز خالی است.", 422);
    public static Error PriceIsEmpty = new("InvalidArguments", "قیمت خالی است.", 422);
    public static Error IdsIsEmpty = new("InvalidArguments", "شناسه ها خالی است.", 422);
    public static Error InValidUnit = new("InvalidArguments", "واحد نرخ ماشین آلات پیمانکار نامعتبر است.", 422);
    public static Error InValidMachinery = new("InvalidArguments", "ماشین آلات نامعتبر است.", 422);
    public static Error InValidIsActive = new("InvalidArguments", "وضعیت نامعتبر است.", 422);
    public static Error InValidActivate = new("InvalidArguments", "وضعیت از قبل فعال است.", 422);
    public static Error InValidInActivate = new("InvalidArguments", "وضعیت از قبل غیر فعال است.", 422);
    public static Error InValidContractorMachinery = new("InvalidArguments", "ماشین آلات پیمانکار نامعتبر است.", 422);
    public static Error InValidContractorMachineryId = new("InvalidArguments", "درخواست نامعتبر است", 422);
    public static Error IsDeleted = new("NotFound", "ماشین آلات پیمانکار حذف شده است.", 204);
    public static Error FilteredContractorMachineryNotFound = new("NotFound", "هیچ ماشین آلات پیمانکاری با این اطلاعات یافت نشد.", 204);
    public static Error UnNumberPlates = new("InvalidArguments", "تعداد کاراکتر های پلاک میبایست 7 رقم  و یک حرف باشد.", 422);

}
