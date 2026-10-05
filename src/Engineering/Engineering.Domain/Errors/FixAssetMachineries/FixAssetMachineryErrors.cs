
namespace Engineering.Domain.Errors;

public static class FixAssetMachineryErrors
{
    public static Error FixAssetMachineryNotFoundWithId = new("NotFound", "موجودی ماشین آلات با این شناسه یافت نشد.", 404);
    public static Error DocumentWithIdNotfound = new("NotFound", "پیوست با این شناسه یافت نشد.", 404);
    public static Error FixAssetMachineryNotWorkNotFoundWithId = new("NotFound", "عدم فعالیتی با این شناسه یافت نشد.", 404);
    public static Error RateNotfound = new("InvalidArguments", "نرخ ماشین یافت نشد.", 404);

    public static Error IdIsEmpty = new("InvalidArguments", "شناسه خالی است.", 422);
    public static Error IdsIsEmpty = new("InvalidArguments", "شناسه ها خالی است.", 422);
    public static Error InValidType = new("InvalidArguments", "نوع موجودی ماشین آلات نامعتبر است.", 422);
    public static Error InValidStatus = new("InvalidArguments", "وضعیت موجودی ماشین آلات نامعتبر است.", 422);
    public static Error InValidMachinery = new("InvalidArguments", "ماشین آلات نامعتبر است.", 422);
    public static Error InValidIsActive = new("InvalidArguments", "وضعیت نامعتبر است.", 422);
    public static Error InValidActivate = new("InvalidArguments", "وضعیت از قبل فعال است.", 422);
    public static Error InValidInActivate = new("InvalidArguments", "وضعیت از قبل غیر فعال است.", 422);
    public static Error InValidRequestCount = new("InvalidArguments", "تعداد درخواست نامعتبر است.", 422);
    public static Error InValidFromDate = new("InvalidArguments", "از تاریخ نامعتبر است.", 422);
    public static Error InValidToDate = new("InvalidArguments", "تا تاریخ نامعتبر است.", 422);
    public static Error InValidDocument = new("InvalidArguments", "تا تاریخ نامعتبر است.", 422);
    public static Error InValidRates = new("InvalidArguments", "نرخ ها نامعتبر میباشند.", 422);
    public static Error InValidNotworkDocument = new("InvalidArguments", "تا تاریخ نامعتبر است.", 422);
    public static Error InValidDates = new("InvalidArguments", "تاریخ پایان از تاریخ شروع کوچیکتر است.", 422);
    public static Error InValidDetailDates = new("InvalidArguments", "از تاریخ از تا تاریخ کوچیکتر است.", 422);
    public static Error InValidFixAssetMachinery = new("InvalidArguments", "موجودی ماشین آلات نامعتبر است.", 422);
    public static Error InValidFixAssetMachineryNotwork = new("InvalidArguments", "عدم کارکرد نامعتبر است.", 422);
    public static Error InValidFixAssetMachineryId = new("InvalidArguments", "درخواست نامعتبر است", 422);
    public static Error InValidFixAssetMachineryNotWorkId = new("InvalidArguments", "عدم کارکرد نامعتبر است", 422);
    public static Error IsDeleted = new("NotFound", "موجودی ماشین آلات حذف شده است.", 204);
    public static Error NotWorkIsDeleted = new("NotFound", "عدم فعالیت موجودی ماشین آلات حذف شده است.", 204);
    public static Error FilteredRateNotFound = new("NotFound", "هیچ نرخی با این اطلاعات یافت نشد.", 204);
    public static Error FilteredFixAssetMachineryNotFound = new("NotFound", "هیچ موجودی ماشین آلاتی با این اطلاعات یافت نشد.", 204);
    public static Error FilteredFixAssetMachineryNotWorkNotFound = new("NotFound", "هیچ عدم فعالیتی با این اطلاعات یافت نشد.", 204);
    public static Error DriverInfoUnValid = new("InvalidArguments", "شما یا نام راننده یا شناسه راننده را وارد کنید.", 422);
    public static Error ContractorIdAndDatesMustAdd = new("InvalidArguments", "در صورت انتخاب نوع اجاره ای باید پیمانکار و تاریخ شروع و پایان را انتخاب کنید.", 422);
    public static Error UnNumberPlates = new("InvalidArguments", "تعداد کاراکتر های پلاک میبایست 7 رقم  و یک حرف باشد.", 422);
    public static Error InvalidRatesRequest = new("InvalidArguments", "نرخ ماشین وارد نشده است.", 404);
    public static Error InvalidRatesDuplicate = new("InvalidArguments", "نرخ ماشین وارد شده با دیگر نرخ ها نداخل دارد است.", 404);
    public static Error InvalidRatesDate = new("InvalidArguments", "تاریخ شروع و پایان نرخ تداخل دارد.", 404);
    public static Error RateIsDeleted = new("InvalidArguments", "نرخ حذف شده است.", 404);
    public static Error RatesDateIsNotValid = new("InvalidArguments", "تاریخ شروع و پایان نرخ در بازه تاریخ شروع و پایان موجودی ماشین وجود ندارد.", 404);
    public static Error RatesDateIsDuplicate = new("InvalidArguments", "تاریخ شروع و پایان نرخ جدید در بازه تاریخ شروع و پایان نرخ های قدیمی وجود دارد.", 404);

}
