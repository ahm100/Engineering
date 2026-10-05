
namespace Engineering.Domain.Errors;

public static class ContractorServiceErrors
{
    public static Error VolumeMustGreaterThan = new("InvalidArguments", "مقدار حجم باید بیشتر از 0 باشد.", 422);
    public static Error VolumeIsEmpty = new("InvalidArguments", "مقدار حجم نباید خالی باشد.", 422);
    public static Error ProjectOperationDetailIdIsEmpty = new("InvalidArguments", "شناسه ریزمتره خالی است.", 422);
    public static Error ProjectOperationDetailIdsIsEmpty = new("InvalidArguments", "شناسه  های ریزمتره خالی است.", 422);
    public static Error ProjectOperationDetailIdGreaterThanZero = new("InvalidArguments", "شناسه ریزمتره باید بزرگتر از صفر باشد.", 422);
    public static Error ServiceIdIsEmpty = new("InvalidArguments", "شناسه خدمت خالی است.", 422);
    public static Error ContractorServiceRequestsIsEmpty = new("InvalidArguments", "دیتای درخواست خالی است.", 422);
    public static Error ServiceIsEmpty = new("InvalidArguments", "پیمانکار مدنظر خدمتی ندارد.", 422);
    public static Error ServiceIsExsist = new("InvalidArguments", "این خدمت در این ریزمتره وجود دارد.", 422);
    public static Error IdIsEmpty = new("InvalidArguments", "شناسه خالی است.", 422);
    public static Error ContactorIdIsEmpty = new("InvalidArguments", "شناسه کارفرما خالی است.", 422);
    public static Error UnValidContactorId = new("InvalidArguments", "پیمانکار نامعتبر است.", 422);
    public static Error ContractorIdMustGreaterThanZero = new("InvalidArguments", "شناسه پیمانکار باید بزرگتر از صفر باشد.", 422);
    public static Error CostCenterIdIsEmpty = new("InvalidArguments", "شناسه مرکزهزینه خالی است.", 422);
    public static Error ProjectIdIsEmpty = new("InvalidArguments", "شناسه پروژه خالی است.", 422);
    public static Error ProjectOperationIdsIsEmpty = new("InvalidArguments", "شناسه شرح عملیات ها خالی است.", 422);
    public static Error ServiceInfoIdsIsEmpty = new("InvalidArguments", "شناسه خدمت ها خالی است.", 422);


    public static Error CanNotDelete = new("InvalidArguments", "شما نمیتوانین یک خدمت را بدون شناسه را حذف کنین.", 422);

    public static Error NotFound = new("NotFound", "هیچ داده ای با این اطلاعات یافت نشد.", 204);
    public static Error WithIdNotFound = new("NotFound", "هیچ خدمات پیمانکار ریزمتره ای با این شناسه یافت نشد.", 404);
    public static Error ContactorsNotActive = new("NotFound", "پیمانکار فعالی با این اطلاعات یافت نشد.", 404);
    public static Error EmploeeNotFound = new("NotFound", "هیچ پرسنلی برای این پیمانکار یافت نشد.", 204);

    public static Error CanNottDelete = new("InvalidArguments", "این برآورد به دلیل داشتن وابستگی اطلاعاتی قابل حذف نمیباشد.", 422);
    public static Error CanNottDeleteForContractorContractDetail = new("InvalidArguments", "به دلیل دارابودن شرح قرارداد پیمانکاری این خدمت ریزمتره قابل حذف نمیباشد.", 422);
    public static Error CanNottDeleteForDailyOperationServices = new("InvalidArguments", "به دلیل دارابودن خدمات روزانه عملیات این خدمت ریزمتره قابل حذف نمیباشد.", 422);
    public static Error ContractorServiceWithIdNotFound = new("NotFound", "خدمت ریزمتره با این شناسه یافت نشد.", 422);
    public static Error IsDeleted = new("NotFound", "خدمت ریزمتره حذف شده است.", 204);
    public static Error IsActive = new("InvalidArguments", "وضعیت فعال میباشد.", 422);
    public static Error IsInactive = new("InvalidArguments", "وضعیت غیرفعال میباشد.", 422);

    public static Error OperationBasedAssignmentIsExist = new("InvalidArguments", "این ریزمتره قبلا به این پیمانکار تخصیص داده شده است.", 422);
    public static Error OperationBasedRemainingVolumeNotAvailable = new("InvalidArguments", "حجم باقیمانده‌ای برای تخصیص این ریزمتره وجود ندارد.", 422);
    public static Error OperationBasedVolumeGreaterThanRemaining = new("InvalidArguments", "حجم وارد شده بیشتر از حجم باقیمانده ریزمتره است.", 422);
    public static Error OperationBasedAssignmentNotFound = new("NotFound", "تخصیص پیمانکار شرح عملیاتی یافت نشد.", 404);

    public static Error OperationBasedAssignmentHasContract = new("InvalidArguments", "به دلیل وجود قرارداد پیمانکار امکان ویرایش این تخصیص وجود ندارد.", 422);

    public static Error OperationBasedDuplicateContractor = new("InvalidArguments", "این ریزمتره قبلا به این پیمانکار تخصیص داده شده است.", 422);
    public static Error MixedContractorServiceTypeNotAllowed = new("InvalidArguments", "امکان ثبت همزمان خدمات خدمتی و شرح عملیاتی برای یک ریزمتره وجود ندارد.", 422);
}
