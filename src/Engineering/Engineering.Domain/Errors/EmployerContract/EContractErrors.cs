
namespace Engineering.Domain.Errors;

public static class EContractErrors
{
    public static Error IsDuplicate = new("Duplicate", "کد قرارداد کارفرما تکراری است.", 409);
    public static Error NotFound = new("NotFound", "هیچ قرارداد کارفرمایی با این شناسه یافت نشد.", 404);


    public static Error StartDateSmallerThanDaily = new("Duplicate", "تاریخ شروع جدید از تاریخ ریزمتره های منتهی به این قرارداد کمتر است.", 409);
    public static Error CanNotAssainge = new("Duplicate", "شرح عملیات پروژه بدون قراردادی که به این قرارداد الحاق کردین، دارای ریزمتره ای با تاریخ شروع کوچکتر است.", 409);
    public static Error ContractCostOverIdIsDuplicate = new("Duplicate", "شناسه ای از هزینه سربار تکراری است.", 409);

    public static Error DateTimeNotValid = new("InvalidArguments", "تاریخ پایان باید بزرگتر از تاریخ شروع باشد.", 422);
    public static Error DateValidate = new("InvalidArguments", "باید هم تاریخ شروع و هم تاریخ پایان مقدار داشته باشید.", 422);
    public static Error EmployerContractWithCodeNotFound = new("NotFound", "هیچ قراردادکارفرمایی با این کد یافت نشد.", 404);
    public static Error EmployerContractWithNameNotFound = new("NotFound", "هیچ قرارداد کارفرمایی با این نام یافت نشد.", 404);
    public static Error FilteredEmployerContractNotFound = new("NotFound", "هیچ قرارداد کارفرمایی با این اطلاعات یافت نشد.", 204);
    public static Error EContractProductNotFound = new("NotFound", "هیچ کالای قرارداد کارفرمایی با این اطلاعات یافت نشد.", 204);
    public static Error EOperationInfoNotFound = new("NotFound", "شرح عملیاتی با این اطلاعات شرح عملیات پروژه یافت نشد.", 204);
    public static Error EContractServiceNotFound = new("NotFound", "هیچ خدمت کارفرمایی با این اطلاعات یافت نشد.", 204);
    public static Error EContractServiceWithIdNotFound = new("NotFound", "هیچ خدمت کارفرمایی با این شناسه یافت نشد.", 204);
    public static Error ContractHaveCode = new("NotFound", "این قرارداد دارای کد میباشد و قابل تغییر نیست.", 204);

    public static Error UnValidId = new("InvalidArguments", "قرارداد کارفرما با این شناسه نامعتبر است.", 422);
    public static Error IdIsEmpty = new("InvalidArguments", "شناسه قرارداد کارفرما خالی است.", 422);
    public static Error ProjectConfirm = new("InvalidArguments", "درصورت اطمینان از تغییر پروژه این قرارداد، حتما تایید نهایی را فعال کنین.", 422);
    public static Error NoContractual = new("InvalidArguments", "پروژه انتخابی شما، قرارداد پذیر نمیباشد.", 422);
    public static Error ProjectOperationConfirm = new("InvalidArguments", "به دلیل تغییر پروژه این قرارداد ارسال شرح عملیات های پروژه این قرارداد اجباری است، لطفا شرح عملیات های پروژه این قرارداد را ارسال کنید.", 422);
    public static Error ContractCostOverIdIsNotValid = new("InvalidArguments", "شناسه ای از هزینه سربار صحیح نمیباشد.", 422);
    public static Error ContractCostOverCanNotIsNotRelated = new("InvalidArguments", "ضریب بالاسری با ردیف 1 نمیتوان تاثیر بگیرد.", 422);
    public static Error OperationInfoIdIsDuplicate = new("Duplicate", "شناسه شرح عملیات در تخصیص و ایجاد شرح عملیات پروژه تکراری میباشد.", 422);
    public static Error EmployerIdIsEmpty = new("InvalidArguments", "شناسه کارفرما خالی است.", 422);
    public static Error ProjectIdIsEmpty = new("InvalidArguments", "شناسه پروژه خالی است.", 422);
    public static Error CostCenterIdIsEmpty = new("InvalidArguments", "شناسه مرکزهزینه خالی است.", 422);
    public static Error ProjectIsEmpty = new("InvalidArguments", "پروژه خالی است", 422);
    public static Error EContractTypeIsEmpty = new("InvalidArguments", "نوع قرارداد کارفرما مشخص نمیباشد.", 422);
    public static Error EOperationIsNotAssigned = new("InvalidArguments", "قرارداد چنین شرح عملیات پروژه ای با چنین شناسه ای ندارد.", 422);
    public static Error WorkLoadIsNotAssigned = new("InvalidArguments", "حجم شرح عملیات پروژه ای خالی میباشد.", 422);
    public static Error PriceIsNotAssigned = new("InvalidArguments", "لطفا روکش مالی را برای همه شرح عملیات ها خود انتخاب کنید .", 422);
    public static Error CurrencyUnitIdIsEmpty = new("InvalidArguments", "واحد ارز خالی است", 422);
    public static Error PenaltyPercentageIsEmpty = new("InvalidArguments", "درصد خطا خالی است", 422);
    public static Error DuplicateOperationCombination = new("InvalidArguments", "شرح عملیات های این قرارداد تکراری است.", 422);
    public static Error DuplicateStatusCombination = new("InvalidArguments", "قرارداد در همین وضعیت میباشد .", 422);
    public static Error DuplicateProjectOperationCombination = new("InvalidArguments", "شرح عملیات های پروژه این قرارداد تکراری است.", 422);
    public static Error DuplicateUnitOfMeasurementCombination = new("InvalidArguments", "واحد های اندازه گیری این قرارداد تکراری است.", 422);
    public static Error DuplicateProductGroupCombination = new("InvalidArguments", "گروه های کالا این قرارداد تکراری است.", 422);
    public static Error IsFinalOrPrimaryManagerConfirmed = new("InvalidArguments", "وضعیت نمیتواند از وضعیت تایید مدیر به تایید مدیر برود.", 422);
    public static Error IsActiveIsEmpty = new("InvalidArguments", "وضعیت خالی است", 422);
    public static Error EmployerContractCodeIsEmpty = new("InvalidArguments", "کد خالی است", 422);
    public static Error IsActive = new("InvalidArguments", "وضعیت قرارداد فعال میباشد.", 422);
    public static Error IsInactive = new("InvalidArguments", "وضعیت قرارداد غیرفعال میباشد.", 422);
    public static Error CanNotDeleteCostOver = new("InvalidArguments", "شما نمیتوانین یک هزینه سربار بدون شناسه را حذف کنین.", 422);
    public static Error CanNotDeleteDucument = new("InvalidArguments", "شما نمیتوانین یک مستند بدون شناسه را حذف کنین.", 422);
    public static Error PriceIsNull = new("InvalidArguments", "برای تغییر وضعیت به تایید کارشناس و یا سرپرست امور قرارداد به قیمت گذاری نیاز است.", 422);
    public static Error WorkloadIsNull = new("InvalidArguments", "برای تغییر وضعیت به تایید مدیر پروژه نیاز به تعیین حجم است.", 422);
    public static Error WorkloadPositive = new("InvalidArguments", "حجم باید مثبت باشد.", 422);
    public static Error PricePositive = new("InvalidArguments", "حجم باید مثبت باشد.", 422);

    public static Error IsDeleted = new("NotFound", "این قرارداد حذف شده است.", 204);
    public static Error CanNottDelete = new("InvalidArguments", "این قرارداد به دلیل داشتن وابستگی اطلاعاتی قابل حذف نمیباشد.", 422);
    public static Error CanNottDeleteBecauseOfProjectOperationDetails = new("InvalidArguments", " به دلیل داشتن شرح عملیات برای عملیات پروژه این قرارداد قابل حذف نمیباشد.", 422);
    public static Error CanNottDeleteBecauseOfConsiderationDependencies = new("InvalidArguments", " به دلیل داشتن خدمات برای ملاحظات کارفرما این قرارداد قابل حذف نمیباشد.", 422);
    public static Error EmployerContractHaveCode = new("InvalidArguments", "این قرارداد به دلیل داشتن کد قرارداد قابل حذف نمیباشد.", 422);

    public static Error UnValidMaxPrice = new("InvalidArguments", "بیشترین قیمت باید مثبت باشد.", 422);
    public static Error UnValidTax = new("InvalidArguments", "هزینه مالیات نمیتواند خالی باشد.", 422);
    public static Error UnValidTaxPercent = new("InvalidArguments", "درصد مالیات نمیتواند خالی باشد.", 422);
    public static Error UnValidAdvancePayment = new("InvalidArguments", "درصد پیش پرداخت نمیتواند خالی یا منفی باشد.", 422);
    public static Error UnValidTransportationCost = new("InvalidArguments", "هزینه حمل و نقل نمیتواند خالی باشد.", 422);
    public static Error UnValidTransportationCostPercent = new("InvalidArguments", "درصد هزینه حمل و نقل نمیتواند خالی باشد.", 422);
    public static Error UnValidIncreaseRate = new("InvalidArguments", "ضریب افزایش نمیتواند خالی باشد.", 422);
    public static Error UnValidProductGroupId = new("InvalidArguments", "شناسه گروه کالاها باید مثبت باشد.", 422);
    public static Error UnValidProductId = new("InvalidArguments", "شناسه کالاها باید مثبت باشد.", 422);
    public static Error UnValidServiceId = new("InvalidArguments", "شناسه خدمت ها باید مثبت باشد.", 422);
}

public static class EmployerDocErrors
{
    public static Error EmployerDochWithIdNotFound = new("NotFound", "هیچ شرح مدارکی با این شناسه یافت نشد.", 404);
    public static Error UnValidFileFormat = new("InvalidArguments", "فرمت فایل شرح مدارک نامعتبر است.", 422);
    public static Error UnValidFileSize = new("InvalidArguments", "فرمت فایل شرح مدارک نامعتبر است.", 422);
    public static Error UnValidId = new("InvalidArguments", "شناسه شرح مدارک نامعتبر است.", 422);
    public static Error IdIsEmpty = new("InvalidArguments", "شناسه شرح مدارک نامعتبر است.", 422);
    public static Error IsDeleted = new("NotFound", "شرح مدارک حذف شده است.", 204);
    public static Error IsActive = new("InvalidArguments", "شرح مدارک فعال است.", 422);
    public static Error IsInactive = new("InvalidArguments", "شرح مدارک غیرفعال است.", 422);
    public static Error FilteredEmployerDocNotFound = new("NotFound", "شرح مدارکی با این اطلاعات یافت نشد.", 204);

    public static Error EmployerDocIdIsEmpty = new("InvalidArguments", "شناسه جزئیات مدارک خالی است!", 422);
    public static Error EmployerContractIsEmpty = new("InvalidArguments", "شناسه قراردادکارفرما خالی است!", 422);
    public static Error DocumentTypeIsEmpty = new("InvalidArguments", "نوع جزئیات مدارک خالی است!", 422);
    public static Error DocumentUrlIsEmpty = new("InvalidArguments", "لینک جزئیات مدارک خالی است!", 422);
    public static Error VersionIsEmpty = new("InvalidArguments", "ورژن جزئیات مدارک خالی است!", 422);
    public static Error IsActiveIsEmpty = new("InvalidArguments", "وضعیت خالی است!", 422);
    public static Error CreateDateIsEmpty = new("InvalidArguments", "تاریخ ایجاد خالی است!", 422);

    public static Error CostCenterIsEmpty = new("InvalidArguments", "نام مرکزهزینه خالی میباشد!", 422);
    public static Error ProjectIsEmpty = new("InvalidArguments", "نام پروژه خالی است!", 422);
    public static Error EmployerNameIsEmpty = new("InvalidArguments", "نام کارفرما خالی است!", 422);
    public static Error DrivePathIsEmpty = new("InvalidArguments", "مسیر درایو خالی است!", 422);
    public static Error ServerAdderssIsEmpty = new("InvalidArguments", "آدرس سرور خالی است!", 422);
    public static Error FileNameIsEmpty = new("InvalidArguments", "نام فایل خالی است!", 422);
}

public static class EmployerConsiderationErrors
{
    public static Error IdIsEmpty = new("InvalidArguments", "شناسه خالی است.", 422);
    public static Error EmployerConsiderationWithIdNotFound = new("NotFound", "هیچ ملاحظات کارفرمایی با این شناسه یافت نشد.", 204);
    public static Error OperationInfoByIdWithIdNotFound = new("NotFound", "هیچ شرح عملیاتی با این شناسه برای ملاحظات کارفرما یافت نشد.", 204);
    public static Error UnValidId = new("InvalidArguments", "شناسه ملاحظات کارفرما نامعتبر است.", 422);
    public static Error IsDeleted = new("NotFound", "ملاحظات کارفرما حذف شده است.", 204);
    public static Error IsActive = new("InvalidArguments", "ملاحظات کارفرما فعال است.", 422);
    public static Error IsInactive = new("InvalidArguments", "ملاحظات کارفرما غیرفعال است.", 422);
    public static Error WithIdNotFound = new("NotFound", "هیچ ملاحظات کارفرمایی با این شناسه یافت نشد.", 404);
    public static Error EmployerConsiderationNotFound = new("NotFound", "هیچ ملاحظات کارفرمایی با فیلتر های مدنظر یافت نشد.", 204);

    public static Error EmployerContractIdIsEmpty = new("InvalidArguments", "شناسه کارفرما خالی است.", 422);
    public static Error EmployerConsiderationIdIsEmpty = new("InvalidArguments", "شناسه خالی است.", 422);
    public static Error ConsiderationTypeEmpty = new("InvalidArguments", "نوع ملاحظات کارفرما خالی است.", 422);
    public static Error NotValidateConsiderationType = new("InvalidArguments", "تایپ مدنظر صحیح نیست.", 422);
    public static Error IsActiveEmpty = new("InvalidArguments", "وضعیت ملاحظات کارفرما خالی است.", 422);

}
public static class EContractStatusErrors
{
    public static Error InValidStatusForProjectManagerResend = new("InvalidArguments", "برای رفتن به وضعیت ارسال مجدد باید در وضعیت برگشت یا  ثبت اولیه باشید.", 422);
    public static Error InValidStatusForProjectManagerPending = new("InvalidArguments", "برای رفتن به وضعیت در انتظار بررسی مدیرپروژه باید در وضعیت ارسال مجدد یا ثبت اولیه باشید ", 422);
    public static Error InValidStatusForProjectManagerReturned = new("InvalidArguments", "برای رفتن به وضعیت برگشت از مدیرپروژه باید در وضعیت در انتظار بررسی باشید ", 422);
    public static Error InValidStatusForProjectManagerConfirmed = new("InvalidArguments", "برای رفتن به وضعیت تایید از مدیرپروژه باید در وضعیت در انتظار بررسی باشید ", 422);
    public static Error InValidStatusForContractExpertPending = new("InvalidArguments", "برای رفتن به وضعیت در انتظار بررسی کارشناس امور قرارداد باید در وضعیت در انتظار بررسی یا تایید مدیر پروژه باشید ", 422);
    public static Error InValidStatusForContractExpertReturned = new("InvalidArguments", "برای رفتن به وضعیت برگشت از کارشناس امور قرارداد باید در وضعیت در انتظار بررسی باشید ", 422);
    public static Error InValidStatusForContractExpertConfirmed = new("InvalidArguments", "برای رفتن به وضعیت تایید از کارشناس امور قرارداد باید در وضعیت در انتظار بررسی باشید ", 422);
    public static Error InValidStatusForContractExpertReturnToUser = new("InvalidArguments", "برای رفتن به وضعیت برگشت به کاربر از کارشناس امور قرارداد باید در وضعیت در انتظار بررسی باشید ", 422);
    public static Error InValidStatusForEmployerReturned = new("InvalidArguments", "برای رفتن به وضعیت برگشت از کارفرما باید بعد از وضعیت تایید مدیر پروژه باشید ", 422);
    public static Error InValidStatusForEmployerPending = new("InvalidArguments", "برای رفتن به وضعیت در انتظار از کارفرما باید بعد از وضعیت تایید مدیر پروژه باشید ", 422);
    public static Error InValidStatusForEmployerConfirmed = new("InvalidArguments", "برای رفتن به وضعیت تایید از کارفرما باید بعد از وضعیت تایید مدیر پروژه باشید ", 422);
    public static Error InValidStatusForContractorSupervisorPending = new("InvalidArguments", "برای رفتن به وضعیت در انتظار بررسی سرپرست امور قرارداد باید در وضعیت تایید مدیر پروژه باشید ", 422);
    public static Error InValidStatusForReturnToProjectManager = new("InvalidArguments", "برای رفتن به وضعیت برگشت به مدیرپروژه باید در وضعیت در انتظار بررسی یا تایید مدیرپروژه باشید باشید ", 422);
    public static Error InValidStatusForContractorSupervisorReturnToUser = new("InvalidArguments", "برای رفتن به وضعیت برگشت به کاربر از سرپرست امور قرارداد باید در وضعیت در انتظار بررسی باشید ", 422);
    public static Error InValidStatusForContractorSupervisorConfirmed = new("InvalidArguments", "برای رفتن به تایید سرپرست امور قرارداد باید در وضعیت در انتظار بررسی باشید ", 422);
    public static Error InValidStatusForPaymentConfirmation = new("InvalidArguments", "برای رفتن به وضعیت صدور دستور باید وضعیت در تایید سرپرست امور قرارداد نهایی باشید ", 422);
    public static Error InValidStatusForPaid = new("InvalidArguments", "برای رفتن به پرداخت شده باید در وضعیت صدور دستور پرداخت باشید ", 422);
    public static Error InValidStatusForRejectPaid = new("InvalidArguments", "برای رفتن به لغو پرداخت باید در وضعیت صدور دستور پرداخت باشید ", 422);
    public static Error InValidStatusForPrimaryManagerReturned = new("InvalidArguments", "برای رفتن به برگشت از مدیر اولیه باید در وضعیت تایید سرپرست امور قرارداد باشید ", 422);
    public static Error InValidStatusForPrimaryManagerConfirmed = new("InvalidArguments", "برای رفتن به تایید مدیر اولیه باید در وضعیت تایید سرپرست امور قرارداد باشید ", 422);
    public static Error InValidStatusForFinalManagerReturned = new("InvalidArguments", "برای رفتن به برگشت مدیر پایانی باید در وضعیت تایید سرپرست امور قرارداد باشید ", 422);
    public static Error InValidStatusForFinalManagerConfirmed = new("InvalidArguments", "برای رفتن به تایید برگشت مدیر پایانی باید در وضعیت تایید سرپرست امور قرارداد باشید ", 422);
    public static Error InValidStatusForInvalidated = new("InvalidArguments", "برای رفتن به ابطال شده باید در وضعیت های مجاز باشید ", 422);
    public static Error InValidStatusForManagementReturnForReview = new("InvalidArguments", "برای رفتن به وضعیت مدنظر در وضعیت تایید کارشناس ارشد باید باشید ", 422);
    public static Error InValidStatusForDelete = new("InvalidArguments", "وضعیت صورت وضعیت پیمانکار شما مناسب حذف نمیباشد.", 422);
}

