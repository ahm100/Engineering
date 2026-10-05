
namespace Engineering.Domain.Errors;

public static class TransportationRequestDocumentErrors
{
    public static Error TransportationRequestDocumentWithIdNotFound = new("NotFound", "مستندات درخواست ترابری یافت نشد.", 404);

    public static Error InValidUrl = new("InvalidArguments", "لینک نامعتبر است.", 422);
    public static Error InValidTransportationRequest = new("InvalidArguments", "درخواست ترابری نامعتبر است.", 422);
    public static Error InValidTransportationRequestDocumentId = new("InvalidArguments", "شناسه نامعتبر است.", 422);
    public static Error IsDeleted = new("NotFound", "مستندات درخواست ترابری حذف شده است.", 204);
}
