
namespace Engineering.Domain.Errors;

public static class FiduciaryProductDetailReturnDocumentErrors
{
    public static Error FiduciaryProductDetailReturnDocumentWithIdNotFound = new("NotFound", "پیوست عودت کالای درخواست امانی یافت نشد.", 404);

    public static Error InValidUrl = new("InvalidArguments", "لینک نامعتبر است.", 422);
    public static Error InValidFiduciaryProductReturnDetail = new("InvalidArguments", "عودت کالای درخواست امانی نامعتبر است.", 422);
    public static Error InValidFiduciaryProductDetaiReturnDocument = new("InvalidArguments", "پیوست عودت کالای درخواست امانی نامعتبر است.", 422);
}
