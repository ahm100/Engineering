
namespace Engineering.Domain.Errors;

public static class MachineTypeErrors
{
    public static Error MachineTypeIsDuplicate = new("Duplicate", "نوع ماشین تکراری است.", 409);
    public static Error TypeNameIsDuplicate = new("Duplicate", "نام نوع ماشین تکراری است.", 409);
    public static Error TypeCodeIsDuplicate = new("Duplicate", "کد نوع ماشین تکراری است.", 409);

    public static Error IdIsEmpty = new("InvalidArguments", "شناسه خالی است.", 422);
    public static Error MachineTypeCodeIsEmpty = new("InvalidArguments", "کد خالی است.", 422);
    public static Error MachineTypeTitleIsEmpty = new("InvalidArguments", "نام خالی است.", 422);
    public static Error FromWeightIsEmpty = new("InvalidArguments", "ماشین از وزن خالی است.", 422);
    public static Error UntilWeightIsEmpty = new("InvalidArguments", "ماشین تا وزن خالی است.", 422);
    public static Error IsActiveIsEmpty = new("InvalidArguments", "وضعیت نوع ماشین خالی است.", 422);
    public static Error CabinTypeCodeIsEmpty = new("InvalidArguments", "کد نوع اتاق خالی است.", 422);
    public static Error CabinTypeCodeMustBeGreaterThanZero = new("InvalidArguments", "کد نوع اتاق باید بزرگتر از صفر باشد.", 422);
    public static Error InValidCabinType = new("InvalidArguments", "نوع اتاق نامعتبر است.", 422);
    public static Error IsActive = new("InvalidArguments", "وضعیت نوع ماشین فعال میباشد.", 422);
    public static Error CanNottDeleteBecauseOfTransportationRequest = new("InvalidArguments", " به دلیل داشتن درخواست ترابری این ماشین قابل حذف نمیباشد.", 422);
    public static Error CanNotDeleteForShippingcost = new("InvalidArguments", "به دلیل استفاده در هزینه ارسال بخش لجستیک پیمانکار این ماشین قابل حذف نیست.", 422);
    public static Error IsInactive = new("InvalidArguments", "وضعیت نوع ماشین غیرفعال میباشد.", 422);

    public static Error MachineTypeIdNotFound = new("NotFound", "شناسه مورد نظر یافت نشد.", 404);
    public static Error MachineTypeWithIdNotFound = new("NotFound", "هیچ نوع ماشین با این شناسه یافت نشد.", 404);
    public static Error MachineTypeWithNpOrVINDuplicate = new("NotFound", "ماشین با پلاک یا شماره شاسی وارد شده تکراریست.", 404);
    public static Error MachineTypeNotFound = new("NotFound", "نوع ماشین مورد نظر یافت نشد.", 404);
    public static Error FilteredMachinTypeNotFound = new("NotFound", "نوع ماشینی یافت نشد.", 204);
    public static Error IsDeleted = new("NotFound", "نوع ماشینی حذف شده است.", 204);
    public static Error CanNotDeleteBecauseOfMachines = new("NotAccess", "به دلیل دارا بودن ماشین این نوع ماشین قابل حذف نمیباشد.", 422);
}
