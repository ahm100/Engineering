
namespace Engineering.Domain.Errors;

public static class RequestGoodsSupplyHistoryErrors
{
    public static Error RequestGoodsSupplyHistoryWithIdNotFound = new("NotFound", "تاریخچه درخواست تامین کالا با این شناسه یافت نشد.", 204);
    public static Error RequestGoodsSupplyDetailHistoryWithIdNotFound = new("NotFound", "تاریخچه جزییات درخواست تامین کالا با این شناسه یافت نشد.", 204);
    public static Error RequestGoodsSupplyManagementHistoryWithIdNotFound = new("NotFound", "تاریخچه کارشناس ارشد جزییات درخواست تامین کالا با این شناسه یافت نشد.", 204);
}
