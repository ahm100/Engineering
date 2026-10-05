
namespace Engineering.Domain.Errors;

public class ContractorContractErrors
{
    public static Error ProjectDoesNotBelongToCompany = new(
        "InvalidArguments",
        "پروژه متعلق به شرکت جاری نمی‌باشد.",
        422);

    public static Error ContractorContractWithIdNotFound = new("NotFound", "قرارداد پیمانکاری با این شناسه یافت نشد.", 404);
    public static Error ContractorContractNotFound = new("NotFound", "قرارداد پیمانکاری با اطلاعات یافت نشد.", 404);
    public static Error ContractorContractContractorsWithFilterNotFound = new("NotFound", "پیمانکار قرارداد پیمانکاری با این اطلاعات یافت نشد.", 404);
    public static Error ContractorsNotFound = new("NotFound", "هیچ پیمانکاری یافت نشد.", 422);
    public static Error VNotFound = new("NotFound", "ورژنی یافت نشد.", 204);
    public static Error ServicePriceNotFound = new("NotFound", "در قرارداد پیمانکار نوع خدمتی شما قیمتی برای خدمت شما یافت نشد.", 422);
    public static Error DateTimeNotValid = new("InvalidArguments", "تاریخ پایان باید بزرگتر از تاریخ شروع باشد.", 422);

    public static Error InValidContractorContractId = new("InvalidArguments", "شناسه قرارداد معتبر نیست.", 422);
    public static Error InValidContractorContractStatus = new("InvalidArguments", "وضعیت خدمات پیمانکار نامعتبراست.", 422);
    public static Error InValidCCForUpdate = new("InvalidArguments", "شما فقط در وضعیت های ثبت اولیه و برگشت قرارداد میتوانین ویرایش انجام دهید.", 422);
    public static Error InValidStatusToPending = new("InvalidArguments", "شما نمیتوانین بجز وضعیت ثبت اولیه یا ارسال مجددبه وضعیت درحال بررسی بروید.", 422);
    public static Error InValidStatusToRejected = new("InvalidArguments", "شما نمیتوانین بجز وضعیت درحال بررسی به وضعیت رد درخواست بروید.", 422);
    public static Error InValidStatusToResended = new("InvalidArguments", "شما نمیتوانین بجز وضعیت رد درخواست به وضعیت برگشت درخواست بروید.", 422);
    public static Error InValidStatusToArchived = new("InvalidArguments", "شما نمیتوانین بجز وضعیت ثبت اولیه یا ارسال مجدد به وضعیت آرشیو بروید.", 422);
    public static Error InValidStatusToConfirmed = new("InvalidArguments", "شما نمیتوانین بجز وضعیت درحال بررسی به وضعیت تایید شده بروید.", 422);

    public static Error InValidUser = new("InvalidArguments", "فقط کاربر درخواست دهنده میتواند ویرایش کند.", 422);
    public static Error InValidDescription = new("InvalidArguments", "توضیح معتبر نیست.", 422);
    public static Error InValidProjectOperationServiceId = new("InvalidArguments", "سرویس معتبر نیست.", 422);
    public static Error InValidProjectOperationDetailId = new("InvalidArguments", "شناسه ریزمتره معتبر نیست.", 422);
    public static Error InValidStatusForDelete = new("InvalidArguments", "شما نمیتوانین بجز وضعیت ثبت اولیه یا رد قراردادی رو حذف کنید.", 422);
    public static Error InValidContractorId = new("InvalidArguments", "پیمانکار معتبر نیست.", 422);

    public static Error POIdsIsNull = new("InvalidArguments", "برای قرارداد مدنظر حداقل باید یک شرح عملیات پروژه انتخاب کنید.", 422);
    public static Error IsPriceListIsFalse = new("InvalidArguments", "شرح عملیات انتخابی شما فهرست بهایی نمیباشد", 422);
    public static Error BasePriceIsZero = new("InvalidArguments", "قیمت پایه برای شرح عملیات شما درج نشده است.", 422);

    public static Error ProjectNoHaveCostCenter = new("InvalidArguments", "مرکزهزینه پروژه شما مشخص نشده است.", 422);
    public static Error InValidContractorIds = new("InvalidArguments", "مجاز به انتخاب خدمات مرتبط با یک پیمانکار هستید.", 422);
    public static Error InValidPercent = new("InvalidArguments", "درصد معتبر نیست.", 422);
    public static Error InValidStartDate = new("InvalidArguments", "تاریخ شروع خالی می باشد.", 422);
    public static Error InValidEndDate = new("InvalidArguments", "تاریخ پایان خالی می باشد.", 422);
    public static Error InValidTotalAmount = new("InvalidArguments", "قیمت قرارداد خالی می باشد.", 422);
    public static Error PriceIsGreaterThanDataType = new("InvalidArguments", "قیمت نمی تواند بیشتر از 16 رقم باشد.", 422);
    public static Error InValidCurrencyId = new("InvalidArguments", "ارز خالی می باشد.", 422);
    public static Error InValidServiceIds = new("InvalidArguments", "خدمات خالی می باشد.", 422);

    public static Error ContractorIdIsEmpty = new("InvalidArguments", "شناسه پیمانکار خالی می باشد.", 422);
    public static Error SkillIdIsEmpty = new("InvalidArguments", "تخصص خالی می باشد.", 422);
    public static Error ThirdPartyIdsIsEmpty = new("InvalidArguments", "پرسنل خالی می باشد.", 422);
    public static Error ThirdPartyIdsNotFound = new("InvalidArguments", "اطلاعات پرسنل درخواستی یافت نشد.", 422);
    public static Error DetailIsEmpty = new("InvalidArguments", "خدمات خالی می باشد.", 422);
    public static Error CostOverIsEmpty = new("InvalidArguments", "هزینه بالاسری خالی می باشد.", 422);
    public static Error ProjectIsEmpty = new("InvalidArguments", "پروژه خالی می باشد.", 422);
    public static Error ProjectMustBiggerThanZero = new("InvalidArguments", "شناسه پروژه باید بزرگتر از صفر باشد.", 422);
    public static Error ServiceInfoIsEmpty = new("InvalidArguments", "خدمات خالی می باشد.", 422);
    public static Error ServiceInfoMustBiggerThanZero = new("InvalidArguments", "شناسه خدمات باید بزرگتر از صفر باشد.", 422);
    public static Error ContractorIsEmpty = new("InvalidArguments", "پیمانکار خالی می باشد.", 422);
    public static Error ContractorMustBiggerThanZero = new("InvalidArguments", "شناسه پیمانکار باید بزرگتر از صفر باشد.", 422);
    public static Error StartDateIsEmpty = new("InvalidArguments", "تاریخ شروع خالی می باشد.", 422);
    public static Error EndDateIsEmpty = new("InvalidArguments", "تاریخ پایان خالی می باشد.", 422);
    public static Error TypeIsEmpty = new("InvalidArguments", "نوع قرارداد درست می باشد.", 422);
    public static Error WorkLoadIsEmpty = new("InvalidArguments", "حجم شرع عملیات معتبر نیست", 422);
    public static Error CurrencyIdIsEmpty = new("InvalidArguments", "شناسه ارز خالی می باشد.", 422);
    public static Error CurrencyIsEmpty = new("InvalidArguments", " ارز خالی می باشد.", 422);
    public static Error PriceIsEmpty = new("InvalidArguments", " قیمت خالی می باشد.", 422);
    public static Error PricesIsEmpty = new("InvalidArguments", " قیمت خالی می باشد.", 422);
    public static Error ContractorContractIdIsEmpty = new("InvalidArguments", "شناسه قرارداد خالی می باشد.", 422);
    public static Error ProjectOperationDetailServiceIdsIsEmpty = new("InvalidArguments", "شناسه سرویس ها خالی می باشد.", 422);
    public static Error IdIsEmpty = new("InvalidArguments", "شناسه قیمت خالی می باشد.", 422);
    public static Error ProjectOperationIdsIsEmpty = new("InvalidArguments", "شناسه عملیات خالی می باشد.", 422);
    public static Error ProjectOperationIdIsEmpty = new("InvalidArguments", "شناسه عملیات خالی می باشد.", 422);
    public static Error ContractorContractRequestIdIsEmpty = new("InvalidArguments", "شناسه درخواست خالی می باشد.", 422);
    public static Error ProjectOperationDetailContractorServiceIdIsEmpty = new("InvalidArguments", "شناسه عملیات خالی می باشد.", 422);
    public static Error TwoPriceAreActive = new("InvalidArguments", "دو قیمت فعال میباشند.", 422);
    public static Error TwoPriceHaveOverlap = new("InvalidArguments", "دو قیمت هم پوشانی دارند.", 422);
    public static Error NoPriceIsActive = new("InvalidArguments", "هیچ قیمتی فعال نیست.", 422);

    public static Error InValidStatusForProjectManagerResend = new("InvalidArguments", "برای رفتن به وضعیت ارسال مجدد باید در وضعیت برگشت یا  ثبت اولیه باشید.", 422);
    public static Error InValidStatusForProjectManagerPending = new("InvalidArguments", "برای رفتن به وضعیت در انتظار بررسی مدیرپروژه باید در وضعیت ارسال مجدد یا ثبت اولیه باشید ", 422);
    public static Error InValidStatusForProjectManagerReturned = new("InvalidArguments", "برای رفتن به وضعیت برگشت از مدیرپروژه باید در وضعیت در انتظار بررسی باشید ", 422);
    public static Error InValidStatusForProjectManagerRejected = new("InvalidArguments", "برای رفتن به وضعیت رد از مدیرپروژه باید در وضعیت در انتظار بررسی باشید ", 422);
    public static Error InValidStatusForProjectManagerConfirmed = new("InvalidArguments", "برای رفتن به وضعیت تایید از مدیرپروژه باید در وضعیت در انتظار بررسی باشید ", 422);
    public static Error InValidStatusForManagementPending = new("InvalidArguments", "برای رفتن به وضعیت در انتظار بررسی کارشناس ارشد باید در وضعیت ارسال مجدد یا ثبت اولیه باشید ", 422);
    public static Error InValidStatusForManagementReturned = new("InvalidArguments", "برای رفتن به وضعیت برگشت از مدیرت باید در وضعیت در انتظار بررسی باشید ", 422);
    public static Error InValidStatusForManagementRejected = new("InvalidArguments", "برای رفتن به وضعیت رد از کارشناس ارشد باید در وضعیت در انتظار بررسی باشید ", 422);
    public static Error InValidStatusForManagementConfirmed = new("InvalidArguments", "برای رفتن به وضعیت تایید از کارشناس ارشد باید در وضعیت در انتظار بررسی باشید ", 422);
    public static Error InValidStatusForManagementReturnForReview = new("InvalidArguments", "برای رفتن به وضعیت مدنظر در وضعیت تایید کارشناس ارشد باید باشید ", 422);
    public static Error InValidStatusForArchived = new("InvalidArguments", "وضعیت شما برای رفتن به در بایگانی درخواست، باید ارسال مجدد یا ثبت اولیه یا رد باشد.", 422);


    public static Error NoThirdPartyFound = new("InvalidArguments", "طرف حسابی برای قرارداد پیمانکار پروژه یافت نشد.", 422);
    public static Error NoThirdPartyFoundForEc = new("InvalidArguments", "طرف حسابی برای قرارداد کارفرما پروژه یافت نشد.", 422);

    public static Error NoContractFoundForPdf = new("NotFound", "قرارداد پیمانکاری برای پروژه یافت نشد.", 422);
    public static Error InValidContractCoefficient = new("InvalidArguments", "ضریب پیمان باید بزرگتر از صفر باشد.", 422);
}
