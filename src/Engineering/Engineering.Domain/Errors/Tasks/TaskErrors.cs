
namespace Engineering.Domain.Errors.Tasks;


public static class TaskErrors
{
 
    public static Error NameIsDuplicate = new("Duplicate", "نام خدمت تکراری است.", 409);
    public static Error TaskNotFound = new("NotFound", "هیچ تسکی با این شناسه یافت نشد.", 404);
    public static Error TaskGroupHasTasks = new("InvalidArguments", "این گروه تسک بخاطر دارابودن وابستگی با تسک  قابل حذف نیست.", 422);
    public static Error NotHaveAccess = new("InvalidArguments", "شما مجوز کافی ندارید.", 401);


    // 
    public static Error TaskGroupNotFound = new("NotFound", "هیچ  گروه تسکی با این شناسه یافت نشد.", 404);


}