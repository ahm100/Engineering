
namespace Engineering.Domain.Errors;

public static class FiduciaryProductHistoryErrors
{
    public static Error FiduciaryProductHistoryWithIdNotFound = new("NotFound", "تاریخچه درخواست امانی یافت نشد.", 404);
    public static Error FiduciaryProductHistoryNotFound = new("NotFound", "هیچ درخواست امانی  با این اطلاعات یافت نشد.", 204);
}
