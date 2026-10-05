
namespace Engineering.Domain.Errors;

public static class ContractorFinancialStatementFiduciaryMachineryErrors
{
    public static Error InvalidPrice = new("InvalidArguments", "قیمت نا معتبر است.", 422);
    public static Error InvalidContractorFinancialStatement = new("InvalidArguments", "صورت وضعیت پیمانکار نا معتبر است.", 422);
    public static Error InValidDailyProjectOperationMachinery = new("InvalidArguments", "کارکرد روزانه ماشین آلات نامعتبر است.", 422);
}
