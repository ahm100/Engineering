
namespace Engineering.Domain.Errors;

public static class ProjectOperationDetailCommentErrors
{
    public static Error ProjectOperationDetailCommentWithIdNotFound = new("NotFound", "نظر با این شناسه یافت نشد.", 404);

    public static Error UnValidLevel = new("InvalidArguments", "سطح بندی نا معتبر است", 422);
    public static Error UnValidProjectOperationDetailId = new("InvalidArguments", "شناسه ریز متر نامعتبر است.", 422);
    public static Error UnValidProjectOperationDetailCommentId = new("InvalidArguments", "شناسه نظر نامعتبر است.", 422);
    public static Error UnValidComment = new("InvalidArguments", "نظر نامعتبر است.", 422);
    public static Error UnValidChildren = new("InvalidArguments", "نظر پیام پاسخ داده شده دارد.", 422);
    public static Error UnValidDocuments = new("InvalidArguments", "پیوست نظر نا معتبر است.", 422);
    public static Error UnValidCreators = new("InvalidArguments", "ایجاد کننده معتبر نیست.", 422);
    public static Error IsDeleted = new("NotFound", "نظر ریزمتره حذف شده است.", 204);
    public static Error IsDeletedForDoc = new("NotFound", "پیوست نظر ریزمتره حذف شده است.", 204);
}
