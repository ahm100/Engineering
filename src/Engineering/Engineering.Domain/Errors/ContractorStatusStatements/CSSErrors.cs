
namespace Engineering.Domain.Errors;

public static class CSSErrors
{
    public static Error InValidCostCenter = new("InvalidArguments", "مرکز معتبر نیست.", 422);
    public static Error InValidStatusForConfirmed = new("InvalidArguments", "برای صدور دستور پرداخت باید تایید مدیر اول و مدیر نهایی را داشته باشید.", 422);
    public static Error InValidConfirmedPaymentDate = new("InvalidArguments", "تاریخ پرداخت خالی است.", 422);
    public static Error InValidSeasonId = new("InvalidArguments", "رسته رشته فصل شما خالی است.", 422);
    public static Error InValidPaymentDate = new("InvalidArguments", "تاریخ پرداخت باید از تاریخ صدور بزرگتر باشد.", 422);
    public static Error InValidRequestContractorId = new("InvalidArguments", "دستور پرداخت فقط برای درخواست هایی که دارای پیمانکار هستند قابل ایجاد است.", 422);
    public static Error InValidConfirmedBankAccountId = new("InvalidArguments", "اطلاعات شبا طرف حساب خالی است.", 422);
    public static Error InValidConfirmedPrice = new("InvalidArguments", "مبلغ پرداختی خالی است.", 422);
    public static Error InValidDescription = new("InvalidArguments", "توضیحات خالی است.", 422);
    public static Error InValidProject = new("InvalidArguments", "پروژه نامعتبر است.", 422);
    public static Error InValidProjectId = new("InvalidArguments", "شناسه پروژه نامعتبر است.", 422);
    public static Error ProjectNotInCostCenter = new("InvalidArguments", "پروژه مدنظر در مرکزهزینه انتخابی نیست.", 422);
    public static Error InValidContractorContract = new("InvalidArguments", "قرارداد پیمانکار با این شناسه یافت نشد.", 422);
    public static Error CanNotMultiPayment = new("InvalidArguments", "شما نمیتوانید وقتی کل مبلغ را تایید میکیند از پرداخت چند بخشی هم استفاده کنید.", 422);
    public static Error InValidContractor = new("InvalidArguments", "پیمانکاری با این شناسه یافت نشد.", 422);
    public static Error AllIsZiro = new("InvalidArguments", "برای پیمانکار مد نظر در بازه مشخص و قرارداد مشخص هیچ دیتایی یافت نشده است.", 422);
    public static Error CanPayableAmount = new("InvalidArguments", "شما نمیتوانین بیشتر از مبلغ مورد محاسبه پرداخت، مبلغ پرداخت داشته باشید.", 422);
    public static Error InValidContractorContractStatus = new("InvalidArguments", "قرارداد پیمانکار شما باید در وضعیت تایید شده باشد.", 422);
    public static Error IsAccsseToInvalidated = new("InvalidArguments", "درصورت ابطال صورت وضعیت تایید شده، حتما اطمینان حاصل کنید.", 422);
    public static Error ContractorNoInContractorContract = new("InvalidArguments", "پیمانکار مد نظر در این قراردادپیمانکار نمیباشد.", 422);
    public static Error InvalidRequestDate = new("InvalidArguments", "تاریخ وارد شده در بازه قرارداد پیمانکار نیست.", 422);
    public static Error ContractorContractTypeNotFound = new("InvalidArguments", "هیچ نوع قراردادی در بازه مشخص شده یافت نشد.", 422);
    public static Error InvalidrefId = new("InvalidArguments", "شناسه نامعتبر است.", 422);
    public static Error InvalidRefrenceId = new("InvalidArguments", "شناسه  نامعتبر است.", 422);
    public static Error ContractorStatusStatementWithIdNotFound = new("NotFound", "شرح عملیات پروژه ای برای این صورت وضعیت پیمانکار یافت نشد.", 404);
    public static Error NoHaveConfirmedContract = new("NotFound", "قرارداد پیمانکار تایید شده ای یافت نشد.", 404);
    public static Error LastIsNull = new("NotFound", "شناسه آخرین صورت وضعیت خالی است.", 404);
    public static Error NotFound = new("NotFound", "صورت وضعیت شما یافت نشد.", 404);
    public static Error PriceNotFound = new("NotFound", "قیمتی برای این شرح عملیات در بازه مشخص شده یافت نشد.", 404);
    public static Error DuplicatePrice = new("Duplicate", "در بازه مشخص شده بیش از یک قیمت در قرارداد پیمانکار وجود دارد.", 409);
    public static Error DailyProjectOperationsNotFound = new("Duplicate", "در بازه مشخص شده هیچ کارکرد روزاته ای یافت نشد.", 409);
    public static Error ContractNotSet = new("Duplicate", "قرارداد آخرین صورت وضعیت شما با قرارداد صورت وضعیت فعلی یکی نیست.", 409);
    public static Error DiscountNotFound = new("Duplicate", "تخفیفی با این شناسه یافت نشد.", 409);
    public static Error DiscountIsEmpty = new("Duplicate", "تخفیف خالی میباشد.", 409);
    public static Error DiscountIdIsNull = new("Duplicate", "شناسه خالی میباشد.", 409);
    public static Error ThirdpartyIdIsNull = new("InvalidArguments", "در صورت انتخاب نشدن تنخواه گردان طرف حساب جهت صدور دستور پرداخت اجباری است.", 422);


    public static Error NoCreatorFound = new("NotFound", "ایجاد کننده ای یافت نشد.", 204);
    public static Error ThirdPartyNotFound = new("NotFound", "شخص یافت نشد.", 404);
    public static Error ContractProjectOperationNotFound = new("NotFound", "شرح عملیات پروژه ای در این بازه و این اطلاعات یافت نشد.", 404);
    public static Error ServicesNotFound = new("NotFound", "کارکرد روزانه ای با این خدمت یافت نشد.", 404);

    public static Error InvalidContractorStatusStatement = new("InvalidArguments", "صورت وضعیت پیمانکار نا معتبر است.", 422);
    public static Error InvalidContractorStatusStatementStatus = new("InvalidArguments", "وضیعت صورت وضعیت پیمانکار نا معتبر است.", 422);
    public static Error InvalidStartDate = new("InvalidArguments", "تاریخ شروع نا معتبر است.", 422);
    public static Error InvalidEndDate = new("InvalidArguments", "تاریخ پایان نا معتبر است.", 422);
    public static Error InvalidTotalPrice = new("InvalidArguments", "قیمت صورت وضعیت نا معتبر است.", 422);
    public static Error InvalidWorkDoneValue = new("InvalidArguments", "درصد انجام کار نا معتبر است.", 422);
    public static Error InvalidContractorContract = new("InvalidArguments", "قرارداد پیمانکار نا معتبر است.", 422);
    public static Error InvalidProjectOperationDetailId = new("InvalidArguments", "ریز متر نا معتبر است.", 422);
    public static Error InvalidProduct = new("InvalidArguments", "کالا نا معتبر است.", 422);
    public static Error InvalidProductGroup = new("InvalidArguments", "گروه محصول نا معتبر است.", 422);
    public static Error InvalidCurrency = new("InvalidArguments", "ارز نا معتبر است.", 422);
    public static Error InvalidHeaderInfo = new("InvalidArguments", "اطلاعات هدر معتبر نیست.", 422);
    public static Error InvalidMeasureunit = new("InvalidArguments", "اطلاعات هدر معتبر نیست.", 422);
    public static Error InValidProjectOperation = new("InvalidArguments", "شرح عملیات پروژه نا معتبر است.", 422);
    public static Error InValidStatus = new("InvalidArguments", "وضعیت نا معتبر است.", 422);
    public static Error JustNew = new("InvalidArguments", "فقط در وضعیت ثبت اولیه میتوانین حذف کنید.", 422);

    public static Error CantAddDiscount = new("InvalidArguments", "در وضعیت های تایید پرداخت و پرداخت کامل امکان تغییر تخفیف نمیباشد.", 422);

    public static Error InValidTransportationRequest = new("InvalidArguments", "درخواست ترابری نامعتبر است.", 422);

    public static Error InValidStatusForUpdate = new("InvalidArguments", "وضعیت شما برای ویرایش مناسب نمیباشد.", 422);
    public static Error InValidStatusForProjectManagerResend = new("InvalidArguments", "برای رفتن به وضعیت ارسال مجدد باید در وضعیت برگشت یا  ثبت اولیه باشید.", 422);
    public static Error InValidStatusForProjectManagerPending = new("InvalidArguments", "برای رفتن به وضعیت در انتظار بررسی مدیرپروژه باید در وضعیت ارسال مجدد یا ثبت اولیه باشید ", 422);
    public static Error InValidStatusForProjectManagerReturned = new("InvalidArguments", "برای رفتن به وضعیت برگشت از مدیرپروژه باید در وضعیت در انتظار بررسی باشید ", 422);
    public static Error InValidStatusForProjectManagerRejected = new("InvalidArguments", "برای رفتن به وضعیت رد از مدیرپروژه باید در وضعیت در انتظار بررسی باشید ", 422);
    public static Error InValidStatusForProjectManagerConfirmed = new("InvalidArguments", "برای رفتن به وضعیت تایید از مدیرپروژه باید در وضعیت در انتظار بررسی باشید ", 422);
    public static Error InValidStatusForManagementPending = new("InvalidArguments", "برای رفتن به وضعیت در انتظار بررسی کارشناس ارشد باید در وضعیت ارسال مجدد یا ثبت اولیه باشید ", 422);
    public static Error InValidStatusForManagementReturned = new("InvalidArguments", "برای رفتن به وضعیت برگشت از مدیرت باید در وضعیت در انتظار بررسی باشید ", 422);
    public static Error InValidStatusForReturnToProjectManager = new("InvalidArguments", "برای رفتن به وضعیت برگشت به مدیرپروژه باید در وضعیت در انتظار بررسی یا تایید مدیرپروژه باشید باشید ", 422);
    public static Error InValidStatusForManagementRejected = new("InvalidArguments", "برای رفتن به وضعیت رد از کارشناس ارشد باید در وضعیت در انتظار بررسی باشید ", 422);
    public static Error InValidStatusForManagementConfirmed = new("InvalidArguments", "برای رفتن به تایید کارشناس ارشد پرداخت باید در وضعیت در انتظار بررسی باشید ", 422);
    public static Error InValidStatusForPaymentConfirmation = new("InvalidArguments", "برای رفتن به وضعیت صدور دستور پرداخت باید وضعیت در تایید کارشناس ارشد نهایی باشید ", 422);
    public static Error InValidStatusForPaid = new("InvalidArguments", "برای رفتن به پرداخت شده باید در وضعیت صدور دستور پرداخت باشید ", 422);
    public static Error InValidStatusForRejectPaid = new("InvalidArguments", "برای رفتن به لغو پرداخت باید در وضعیت صدور دستور پرداخت باشید ", 422);
    public static Error InValidStatusForPrimaryManagerRejected = new("InvalidArguments", "برای رفتن به رد کارشناس ارشد اولیه باید در وضعیت تایید کارشناس ارشد باشید ", 422);
    public static Error InValidStatusForPrimaryManagerConfirmed = new("InvalidArguments", "برای رفتن به تایید کارشناس ارشد اولیه باید در وضعیت تایید کارشناس ارشد باشید ", 422);
    public static Error InValidStatusForFinalManagerRejected = new("InvalidArguments", "برای رفتن به رد کارشناس ارشد نهایی باید در وضعیت تایید کارشناس ارشد اولیه باشید ", 422);
    public static Error InValidStatusForFinalManagerConfirmed = new("InvalidArguments", "برای رفتن به تایید کارشناس ارشد نهایی باید در وضعیت تایید کارشناس ارشد اولیه باشید ", 422);
    public static Error InValidStatusForInvalidated = new("InvalidArguments", "برای رفتن به ابطال شده باید در وضعیت های مجاز باشید ", 422);
    public static Error InValidStatusForManagementReturnForReview = new("InvalidArguments", "برای رفتن به وضعیت مدنظر در وضعیت تایید کارشناس ارشد باید باشید ", 422);

    public static Error InValidStatusForDelete = new("InvalidArguments", "وضعیت صورت وضعیت پیمانکار شما مناسب حذف نمیباشد.", 422);

}
