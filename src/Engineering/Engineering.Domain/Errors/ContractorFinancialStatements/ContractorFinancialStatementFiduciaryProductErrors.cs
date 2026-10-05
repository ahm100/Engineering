
namespace Engineering.Domain.Errors;

public static class ContractorFinancialStatementFiduciaryProductErrors
{
    public static Error InvalidPrice = new("InvalidArguments", "قیمت نا معتبر است.", 422);
    public static Error InvalidFiduciaryProductDetail = new("InvalidArguments", "امانی نا معتبر است.", 422);
    public static Error InvalidFiduciaryProductDetailReturn = new("InvalidArguments", "عودت امانی نا معتبر است.", 422);
    public static Error InvalidContractorFinancialStatement = new("InvalidArguments", "صورت وضعیت پیمانکار نا معتبر است.", 422);
}
