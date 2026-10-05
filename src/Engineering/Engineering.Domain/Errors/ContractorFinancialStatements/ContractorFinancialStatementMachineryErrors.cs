
namespace Engineering.Domain.Errors;

public static class ContractorFinancialStatementMachineryErrors
{
    public static Error InvalidPrice = new("InvalidArguments", "قیمت نا معتبر است.", 422);
    public static Error InvalidTotalWorkDone = new("InvalidArguments", "کل کارکرد نا معتبر است.", 422);
    public static Error InvalidTotalCount = new("InvalidArguments", "تعداد کل نا معتبر است.", 422);
    public static Error InvalidCurrency = new("InvalidArguments", "ارز نا معتبر است.", 422);
    public static Error InvalidProjectOperation = new("InvalidArguments", "شرح عملیات نا معتبر است.", 422);
    public static Error InvalidMachinery = new("InvalidArguments", "ماشین آلات نا معتبر است.", 422);
    public static Error InvalidContractorFinancialStatement = new("InvalidArguments", "صورت وضعیت پیمانکار نا معتبر است.", 422);
}
