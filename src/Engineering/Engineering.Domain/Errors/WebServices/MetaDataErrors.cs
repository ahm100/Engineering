
namespace Engineering.Domain.Errors;

public static class MetaDataErrors
{
    public static Error MeasureunitWithIdNotFound = new("NotFound", "هیچ واحد اندازگیری با این شناسه یافت نشد.", 404);
    public static Error EmployerWithIdNotFound = new("NotFound", "هیچ کارفرمایی با این شناسه یافت نشد.", 404);
    public static Error EmployerUniqueCodeIsNull = new("NotFound", "کد انحصاری کارفرما خالی است.", 404);
    public static Error EmployerValueIsNull = new("NotFound", "اطلاعات کارفرما خالی است.", 404);
    public static Error IdIsEmpty = new("NotFound", "شناسه خالی است.", 422);
    public static Error ProjectOperationIdIsEmpty = new("NotFound", "شناسه شرح عملیات پروژه خالی میباشد.", 422);
    public static Error CodeIsEmpty = new("NotFound", "کد خالی است.", 422);
    public static Error ConnectionLost = new("NotFound", "ارتباط با سرور اطلاعات با مشکل رو به رو شد، دوباره تلاش بفرمایید.", 404);
    public static Error CurrencyWithIdNotFound = new("NotFound", "هیچ ارزی با این شناسه یافت نشد.", 404);

}