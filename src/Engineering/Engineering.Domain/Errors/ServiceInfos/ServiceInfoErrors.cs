
namespace Engineering.Domain.Errors;

public static class ServiceInfoErrors
{
    public static Error NameIsDuplicate = new("Duplicate", "نام خدمت تکراری است.", 409);
    public static Error AdminEnNameIsRequired = new("InvalidArguments", "نام انگلیسی برای خدمت اداری اجباری است.", 409);
    public static Error NameandUnitIsDuplicate = new("Duplicate", "این نام خدمت با این واحد ثبت شده است لطفا از موارد دیگری استفاده کنید.", 409);
    public static Error CodeIsDuplicate = new("Duplicate", "کد خدمت تکراری است.", 409);
    public static Error MeasureUnitDataIsDuplicate = new("Duplicate", ",واحد خدمت تکراری است.", 409);

    public static Error IsActive = new("InvalidArguments", "وضعیت خدمت فعال میباشد.", 422);
    public static Error IsInactive = new("InvalidArguments", "وضعیت خدمت غیرفعال میباشد.", 422);
    public static Error CanNotDelete = new("InvalidArguments", "به دلیل داشتن وابستگی اطلاعاتی نمیتوانین این خدمت را حذف کنید.", 422);
    public static Error CanNotDeleteForContractorServices = new("InvalidArguments", "این خدمت بخاطر دارابودن وابستگی با خدمت پیمانکاران قابل حذف نیست.", 422);
    public static Error CanNotDeleteForOperationInfoServices = new("InvalidArguments", "این خدمت بخاطر دارا بودن وابستگی با خدمت های شرح عملیات قابل حذف نیست.", 422);

    public static Error ServiceInfoWithIdNotFound = new("NotFound", "هیچ خدمتی با این شناسه یافت نشد.", 404);
    public static Error ServiceInfoWithCodeNotFound = new("NotFound", "هیچ خدمتی با این نام یافت نشد.", 404);
    public static Error ServiceInfoWithNameNotFound = new("NotFound", "هیچ خدمتی با این نام یافت نشد.", 404);
    public static Error ServiceInfoWithMeasureUnitNotFound = new("NotFound", "هیچ خدمتی با این نام یافت نشد.", 204);
    public static Error FilteredServiceInfoNotFound = new("NotFound", "هیچ خدمتی با این اطلاعات یافت نشد.", 204);

    public static Error AdministrativeMeasureNotFound = new("NotFound", "واحد اندازه گیری اداری یافت نشد.", 204);

    public static Error UnValidId = new("InvalidArguments", "شناسه خدمت نامعتبر است.", 422);
    public static Error OperationInfoIds = new("InvalidArguments", "شناسه ای از شرح عملیات ها نامعتبر است.", 422);
    public static Error OperationInfoId = new("InvalidArguments", "شناسه شرح عملیات نامعتبر است.", 422);
    public static Error IdIsEmpty = new("InvalidArguments", "شناسه خدمت نامعتبر است.", 422);
    public static Error ServiceInfoNameIsEmpty = new("InvalidArguments", "نام خدمت خالی است.", 422);
    public static Error ServiceInfoCodeIsEmpty = new("InvalidArguments", "کد خدمت خالی است.", 422);
    public static Error UnitOfMeasurementIdIsEmpty = new("InvalidArguments", "شناسه واحد اندازگیری خالی است.", 422);
    public static Error IsActiveIsEmpty = new("InvalidArguments", "وضعیت خالی است.", 422);
    public static Error IsDeleted = new("NotFound", "خدمت حذف شده است.", 204);


}