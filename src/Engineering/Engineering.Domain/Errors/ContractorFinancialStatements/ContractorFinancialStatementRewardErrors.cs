
namespace Engineering.Domain.Errors;

public static class ContractorFinancialStatementRewardErrors
{
    public static Error InvalidRequestReward = new("InvalidArguments", "پاداش جریمه نا معتبر است.", 422);
    public static Error InvalidContractorFinancialStatement = new("InvalidArguments", "صورت وضعیت پیمانکار نا معتبر است.", 422);
}
