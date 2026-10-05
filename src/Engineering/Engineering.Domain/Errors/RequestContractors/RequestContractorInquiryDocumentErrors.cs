
namespace Engineering.Domain.Errors;

public static class RequestContractorInquiryDocumentErrors
{
    public static Error RequestContractorInquiryDocumentWithIdNotFound = new("NotFound", "پیوست درخواست استعلام پیمانکار یافت نشد", 404);
}
