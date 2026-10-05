
namespace Engineering.Domain.Errors;

public static class CostCenterVirtualGroupAdminErrors
{
    public static Error CostCenterVirtualGroupAdminNotFound = new("NotFound", "مدیر گروه مجازی یافت نشد.", 404);

    public static Error InValidCostCenterVirtualGroupAdmin = new("InvalidArguments", "مدیر نامعتبر است.", 422);
    public static Error InValidThirdParty = new("InvalidArguments", "شناسه مدیر نامعتبر است.", 422);
    public static Error InValidUserName = new("InvalidArguments", "نام کاربری نامعتبر است.", 422);
    public static Error InValidCostCenterVirtualGroup = new("InvalidArguments", "گروه مجازی نامعتبر است.", 422);
    public static Error IsDeleted = new("NotFound", "مدیر گروه مجازی مرکزهزینه قابل حذف نمیباشد.", 204);
}
