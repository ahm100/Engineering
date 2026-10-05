
namespace Engineering.Domain.Errors;

public static class ContractorFinancialStatementProductErrors
{
    public static Error InvalidPrice = new("InvalidArguments", "قیمت نا معتبر است.", 422);
    public static Error InvalidProductId = new("InvalidArguments", "محصول نا معتبر است.", 422);
    public static Error InvalidProductGroupId = new("InvalidArguments", "گروه محصول نا معتبر است.", 422);
    public static Error InvalidContractorFinancialStatement = new("InvalidArguments", "صورت وضعیت پیمانکار نا معتبر است.", 422);
    public static Error InvalidRequestGoodsSupplyDetail = new("InvalidArguments", "درخواست تامین کالا نا معتبر است.", 422);
}
