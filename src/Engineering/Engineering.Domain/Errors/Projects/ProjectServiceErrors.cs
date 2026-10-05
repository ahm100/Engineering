
namespace Engineering.Domain.Errors;

public static class ProjectServiceErrors
{
    public static Error ProjectServiceIsDuplicate = new("Duplicate", "خدمات پروژه با پیمانکار انتخابی تکراری است.", 409);

    public static Error IdIsEmpty = new("InvalidArguments", "شناسه خالی است.", 422);
    public static Error OperationInfoServicesNotValid = new("InvalidArguments", "خدمت های انتخابی شرح عملیات شما مجاز نمیباشند.", 422);
    public static Error InvalidService = new("InvalidArguments", "خدمات نامعتبر است.", 422);
    public static Error ProjectNotCollectiveService = new("InvalidArguments", "پروژه شما دسترسی خدمت تجمیعی را ندارد از مدیر پروژه درخواست کنید تا این قابلیت را فعال کند.", 422);
    public static Error InvalidContractor = new("InvalidArguments", "پیمانکار نامعتبر است.", 422);
    public static Error ProjectIsEmpty = new("InvalidArguments", "پروژه خالی است.", 422);
    public static Error ProjectServiceIsEmpty = new("InvalidArguments", "خدمات پروژه خالی است.", 422);
    public static Error ServiceIsEmpty = new("InvalidArguments", "خدمات خالی است.", 422);
    public static Error OperationInfoServiceIsEmpty = new("InvalidArguments", "خدمت شرح عملیات خالی است.", 422);
    public static Error ContractorIsEmpty = new("InvalidArguments", " پیمانکار خالی است.", 422);
    public static Error IsActiveIsEmpty = new("InvalidArguments", "وضعیت خالی است.", 422);
    public static Error ProjectServiceWithProjectIdNotFound = new("NotFound", "هیچ خدمات پروژه ای با این شناسه پروژه یافت نشد.", 204);
    public static Error ContractorsNotFound = new("NotFound", "هیچ پیمانکاری یافت نشد.", 204);
    public static Error ServiceWithProjectIdNotFound = new("NotFound", "هیچ خدماتی یافت نشد.", 204);
    public static Error ProjectServiceWithIdNotFound = new("NotFound", "هیچ خدمات پروژه ای با این شناسه یافت نشد.", 404);
    public static Error ProjectServicesNotFound = new("NotFound", "هیچ خدمات پروژه ای با این اطلاعات یافت نشد.", 204);
    public static Error IsInactive = new("InvalidArguments", "وضعیت خدمات پروژه غیرفعال میباشد.", 422);
    public static Error IsActive = new("InvalidArguments", "وضعیت خدمات پروژه فعال میباشد.", 422);
    public static Error IsDeleted = new("NotFound", "این خدمات پروژه حذف شده است.", 404);
    public static Error HaveContracts = new("NotFound", "شما نمیتوانین خدمتی که دارای قرارداد پیمانکار میباشد را حذف کنین.", 404);
    public static Error HavePOD = new("NotFound", "شما نمیتوانین خدمتی که دارای براورد میباشد را حذف کنین.", 404);
    public static Error HaveDaily = new("NotFound", "شما نمیتوانین خدمتی که دارای کارکردروزانه میباشد را حذف کنین.", 404);
}