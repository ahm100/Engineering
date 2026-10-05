
namespace Engineering.Domain.Errors;

public static class ContractorFinancialStatementThirdPartyErrors
{
    public static Error InvalidThirdPartyId = new("InvalidArguments", "پرسنل نا معتبر است.", 422);
    public static Error InvalidSkillId = new("InvalidArguments", "تخصص نا معتبر است.", 422);
    public static Error InvalidPrice = new("InvalidArguments", "قیمت محصول نا معتبر است.", 422);
    public static Error InvalidContractorFinancialStatement = new("InvalidArguments", "صورت وضعیت پیمانکار نا معتبر است.", 422);
    public static Error InvalidCurrencyId = new("InvalidArguments", "ارز نا معتبر است.", 422);
    public static Error InvalidCount = new("InvalidArguments", "کارکرد پرسنل نا معتبر است.", 422);
}
