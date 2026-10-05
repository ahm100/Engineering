
namespace Engineering.Domain.Errors;

public static class ProjectOperationDetailCommentDocumentErrors
{
    public static Error UnValidProjectOperationDetailCommentDocumentId = new("InvalidArguments", "شناسه پیوست نظر نامعتبر است.", 422);
    public static Error UnValidProjectOperationDetailCommentId = new("InvalidArguments", "شناسه نظر نامعتبر است.", 422);
    public static Error UnValidUrl = new("InvalidArguments", "لینک نامعتبر است.", 422);
}
