
namespace Engineering.Domain.Errors;

public static class FiduciaryProductDetailManagementErrors
{
    public static Error FiduciaryProductDetailManagementWithIdNotFound = new("NotFound", "انبار یافت نشد", 404);

    public static Error InValidInvoiceId = new("InvalidArguments", "شناسه فاکتور انبار نامعتبر است", 422);
    public static Error InValidWarehouseId = new("InvalidArguments", "شناسه انبار نامعتبر است", 422);
    public static Error InValidConfirmedLoanCount = new("InvalidArguments", "مقدار مورد تایید نامعتبر است.", 422);
    public static Error InValidFiduciaryProductDetail = new("InvalidArguments", "کالای امانی نامعتبر است", 422);
    public static Error InValidStatus = new("InvalidArguments", "وضعیت نامعتبر است", 422);
}
