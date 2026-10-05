
namespace Engineering.Domain.Errors;

public static class RequestMachineryInquiryDocumentErrors
{
    public static Error RequestMachineryInquiryDocumentWithIdNotFound = new("NotFound", "پیوست درخواست استعلام ماشین آلات یافت نشد", 404);
}
