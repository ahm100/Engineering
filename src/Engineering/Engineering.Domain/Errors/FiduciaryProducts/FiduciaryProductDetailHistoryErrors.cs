
namespace Engineering.Domain.Errors;

public static class FiduciaryProductDetailHistoryErrors
{
    public static Error FiduciaryProductDetailHistoryWithIdNotFound = new("NotFound", "تاریخچه کالای درخواست امانی یافت نشد.", 204);
}
