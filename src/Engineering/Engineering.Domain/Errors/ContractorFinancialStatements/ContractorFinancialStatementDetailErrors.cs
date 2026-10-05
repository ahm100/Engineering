
namespace Engineering.Domain.Errors;

public static class ContractorFinancialStatementDetailErrors
{
    public static Error InvalidFinalAmount = new("InvalidArguments", "حجم صورت وضعیت نا معتبر است.", 422);
    public static Error InvalidTotalFinalAmount = new("InvalidArguments", "حجم کل انجام شده نا معتبر است.", 422);
    public static Error InvalidTotalPrice = new("InvalidArguments", "مبلغ نا معتبر است.", 422);
    public static Error InvalidStatus = new("InvalidArguments", "وضعیت نا معتبر است.", 422);
    public static Error InvalidProjectOperation = new("InvalidArguments", "شرح عملیات نا معتبر است.", 422);
    public static Error InvalidContractorFinancialStatement = new("InvalidArguments", "صورت وضعیت پیمانکار نا معتبر است.", 422);
}
