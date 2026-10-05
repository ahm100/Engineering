
namespace Engineering.Domain.Errors;

public static class ContractorFinancialStatementDailyOperationErrors
{
    public static Error InvalidPrice = new("InvalidArguments", "مبلغ نا معتبر است.", 422);
    public static Error InvalidDailyProjectOperation = new("InvalidArguments", "کارکرد روزانه نا معتبر است.", 422);
    public static Error InvalidContractorFinancialStatementDetail = new("InvalidArguments", "صورت وضعیت پیمانکار نا معتبر است.", 422);
}
