
namespace Engineering.Domain.Errors;

public static class ProjectAssistantErrors
{
    public static Error UnValidId = new("InvalidArguments", "شناسه دستیار پروژه نامعتبر است.", 422);
    public static Error ProjectAssistanWithIdNotFound = new("NotFound", "هیچ دستیار پروژه ای با این شناسه یافت نشد.", 404);
    public static Error ProjectChildNotFound = new("NotFound", "پروژه انتخابی زیر شاخه ای ندارد.", 204);

    public static Error ProjectIsEmpty = new("InvalidArguments", "پروژه خالی است!", 422);
    public static Error ImplementationAssistantUserIdIsEmpty = new("InvalidArguments", "شناسه کاربر خالی میباشد!", 422);
    public static Error IsDeleted = new("Notfound", "دستیار پروژه حذف شده است.", 204);
    public static Error IsDeletedTecnical = new("Notfound", "دستیار فنی پروژه حذف شده است.", 204);
}