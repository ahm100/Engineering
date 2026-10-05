
namespace Engineering.Domain.Errors;

public static class RequestMachineryErrors
{
    public static Error NotAllowedToConfirm = new("NotFound", "شما برای تایید دسترسی ندارید.", 404);
    public static Error RequestMachineryNotFound = new("NotFound", "درخواست ماشین آلات یافت نشد.", 404);
    public static Error RequestMachineryOperatorNotFound = new("NotFound", "متصدی ماشین آلات یافت نشد.", 404);
    public static Error RequestMachinerysNotFound = new("NotFound", "درخواست های ماشین آلات یافت نشد.", 204);
    public static Error RequestMachineryDocumentWithIdNotFound = new("NotFound", "پیوست درخواست ماشین آلات یافت نشد", 404);
    public static Error RequestMachineryDocumentsWithIdNotFound = new("NotFound", "پیوست های درخواست ماشین آلات یافت نشد", 404);
    public static Error RequestMachineryBillDocumentWithIdNotFound = new("NotFound", "پیوست قبض درخواست ماشین آلات یافت نشد", 404);

    public static Error UnvalidUnit = new("InvalidArguments", "نوع درخواست با نوع رزرو متفاوت است.", 422);
    public static Error TimeSpantCountError = new("InvalidArguments", "زمان مورد نیاز باید با فرمت 00:00 وارد شود.", 422);
    public static Error TimeMoreThan59Min = new("InvalidArguments", "شما نمیتوانین دقیقه را بیشتر از 59 وارد کنید.", 422);

    public static Error InValidTimeRequired = new("InvalidArguments", "زمان مورد نیاز نامعتبر است.", 422);
    public static Error InValidMachineryIdentifier = new("InvalidArguments", "شناسه پلاک ماشین آلات نمیتواند خالی باشد.", 422);
    public static Error ExistsIdentifierInList = new("InvalidArguments", "شناسه پلاکی از ماشین آلات وارد شده تکراری است.", 422);
    public static Error InValidUnit = new("InvalidArguments", "واحد نامعتبر است.", 422);
    public static Error InValidStatus = new("InvalidArguments", "وضعیت نامعتبر است.", 422);
    public static Error InDailyOperations = new("InvalidArguments", "شناسه پلاک ماشین آلات به دلیل داشتن کارکرد روزانه قابل ویرایش نیست.", 422);
    public static Error InValidRequestCount = new("InvalidArguments", "تعداد درخواست نامعتبر است.", 422);
    public static Error InValidFromDate = new("InvalidArguments", "از تاریخ نامعتبر است.", 422);
    public static Error InValidToDate = new("InvalidArguments", "تا تاریخ نامعتبر است.", 422);
    public static Error InValidDates = new("InvalidArguments", "تاریخ پایان از تاریخ شروع کوچیکتر است.", 422);
    public static Error InValidConfirmDates = new("InvalidArguments", "تاریخ پایان تایید از تاریخ شروع تایید کوچیکتر است.", 422);
    public static Error InValidCostCenter = new("InvalidArguments", "مرکز هزینه نامعتبر است.", 422);
    public static Error InValidProject = new("InvalidArguments", "پروژه نامعتبر است.", 422);
    public static Error InValidProjectOperation = new("InvalidArguments", "شرح عملیات پروژه نامعتبر است.", 422);
    public static Error InValidMachineriesGroup = new("InvalidArguments", "گروه ماشین آلات نامعتبر است.", 422);
    public static Error InValidMachinery = new("InvalidArguments", "ماشین نامعتبر است.", 422);
    public static Error InValidId = new("InvalidArguments", "شناسه نامعتبر است.", 422);
    public static Error InValidMachineryidentifier = new("InvalidArguments", "شناسه ماشین مصرفی نامعتبر است.", 422);
    public static Error InValidRequestMachinery = new("InvalidArguments", "درخواست ماشین آلات نامعتبر است.", 422);
    public static Error InValidDocument = new("InvalidArguments", "پیوست نامعتبر است.", 422);
    public static Error InValidFromTime = new("InvalidArguments", "زمان شروع نامعتبر است.", 422);
    public static Error InValidToTime = new("InvalidArguments", "زمان پایان نامعتبر است.", 422);
    public static Error MustBeforeToTime = new("InvalidArguments", "زمان شروع باید قبل از زمان پایان عملیات باشد.", 422);
    public static Error InValidProjectOperationDetail = new("InvalidArguments", "شرح عملیات متناسب با ریز متره انتخاب نشده است.", 422);
    public static Error FromDateNotBiggerThanDetailDate = new("InvalidArguments", "تاریخ شروع درخواست باید بزرگتر یا برابر با تاریخ شروع ریزمتره انتخابی باشد.", 422);
    public static Error InValidRequestMachineryId = new("InvalidArguments", "شناسه درخواست نامعتبر است", 422);
    public static Error InValidRequestMachineryIds = new("InvalidArguments", "شناسه های درخواست نامعتبر است", 422);
    public static Error InValidOperatorAppointmentId = new("InvalidArguments", "متصدی استعلام نامعتبر است", 422);
    public static Error InValidOperatorAppointmentUserId = new("InvalidArguments", "کاربر متصدی نامعتبر است", 422);
    public static Error InValidInquiryStatus = new("InvalidArguments", "وضعیت ارسالی می تواند استعلام گیری یا انتصاب متصدی باشد", 422);
    public static Error InValidForDailyMachineries = new("InvalidArguments", "این درخواست بدلیل داشتن کارکرد روزانه قابل تغییر نیست", 422);
    public static Error IsDeleted = new("NotFound", "درخواست ماشین آلات حذف شده است.", 204);
    public static Error OperatorNotfound = new("NotFound", "درخواست ماشین هیچ متصدی ندارد.", 204);
    public static Error FilteredMachineryRequestNotFound = new("NotFound", "هیچ درخواست دهنده ماشین آلاتی با این اطلاعات یافت نشد.", 204);
    public static Error FilteredMachineryContractorNotFound = new("NotFound", "هیچ پیمانکار ماشین آلاتی با این اطلاعات یافت نشد.", 204);
    public static Error MoreThan59Min = new("InvalidArguments", "شما نمیتوانین دقیقه را بیشتر از 59 وارد کنید.", 422);
    public static Error OperatorUnvalid = new("InvalidArguments", "متصدی نامعتبر است.", 422);
    public static Error IsExist = new("InvalidArguments", "درخواست ماشین آلات تکراریست و وجود دارد.", 422);
    public static Error InValidTimeRequiredCount = new("InvalidArguments", "زمان مورد نیاز باید با فرمت 00:00 وارد شود.", 422);
    public static Error InValidHourlyRate = new("InvalidArguments", "واحد ساعتیست اما نرخ ساعتی خالی میباشد", 422);
    public static Error InValidDailyRate = new("InvalidArguments", "واحد روزانه است اما نرخ روزانه خالی میباشد", 422);
    public static Error InValidServiceRate = new("InvalidArguments", "واحد سرویسی است اما نرخ سرویسی خالی میباشد", 422);
    public static Error InValidRate = new("InvalidArguments", "نرخ خالی میباشد", 422);
}
