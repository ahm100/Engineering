
namespace Engineering.Domain.Errors;

public class ContractorContractDetailErrors
{

    public static Error ContractorContractDetailWithIdNotFound = new("NotFound", "جزئیات قرارداد پیمانکار شما با  این شناسه یافت نشد.", 404);
    public static Error ContractorContractDetailNoHavePrices = new("NotFound", "جزئیات قرارداد پیمانکارهیچ قیمت گذاری ندارد لطفا قیمت گذاری شود.", 404);
    public static Error ContractorContractDetailHaveCSS = new("NotFound", "جزئیات قرارداد شما دارای صورت وضعیت هست لطفا ابتدا صورت وضعیت خودرا حذف کنید.", 404);
    public static Error ContractorContractDetailServiceHaveCSS = new("NotFound", "جزئیات خدمت قرارداد شما دارای صورت وضعیت هست لطفا ابتدا صورت وضعیت خودرا حذف کنید.", 404);
    public static Error CanNotDeleteContractorContractDetailService = new("NotFound", "شما نمیتوانین تنها جزئیات خدمت قرارداد خودر ا حذف کنین جزئیات قرارداد را حذف کنید", 404);

    public static Error InvalidContractorContractDetailId = new("InvalidArguments", "شناسه معتبر نیست.", 422);

    public static Error InvalidProjectOperationDetailContractorServiceId = new("InvalidArguments", "شناسه سرویس معتبر نیست.", 422);

    public static Error ReportNotfound = new("NotFound", "داده ای با فیلتر ارسالی یافت نشد.", 204);
    public static Error IsDeleted = new("NotFound", "شرح قرارداد پیمانکار حذف شده است.", 204);
    public static Error JustOneActivePrice = new("NotFound", "شما فقط یک قیمت فعال میتوانین داشته باشید لطفا قیمت خود را تصحیح کنین", 422);

}
