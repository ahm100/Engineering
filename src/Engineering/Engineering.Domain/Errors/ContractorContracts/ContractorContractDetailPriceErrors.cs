
namespace Engineering.Domain.Errors;

public class ContractorContractDetailPriceErrors
{
    public static Error ContractorContractDetailPriceWithIdNotFound = new("NotFound", "این شناسه یافت نشد.", 404);

    public static Error InvalidContractorContractDetailPriceId = new("InvalidArguments", "شناسه معتبر نیست.", 422);
    public static Error InvalidStartDate = new("InvalidArguments", "تاریخ شروع خالی می باشد.", 422);
    public static Error InvalidEndDate = new("InvalidArguments", "تاریخ پایان خالی می باشد.", 422);
    public static Error InvalidCurrencyId = new("InvalidArguments", "قیمت خالی می باشد.", 422);
    public static Error InvalidPrice = new("InvalidArguments", "ارز خالی می باشد.", 422);
    public static Error IsDeleted = new("NotFound", "شرح مالی قرارداد پیمانکار حذف شده است.", 204);
}
