
namespace Engineering.Domain.Errors;

public static class DailyProjectOperationErrors
{
    public static Error DailyProjectOperationWithIdNotFound = new("NotFound", "عملکرد روزانه یافت نشد.", 404);
    public static Error ProjectOperationWithIdNotFound = new("NotFound", "شرح عملیات یافت نشد.", 404);
    public static Error ProjectOperationDetailWithIdNotFound = new("NotFound", "ریز متره یافت نشد.", 404);
    public static Error DailyProjectOperationFilteredNotFound = new("NotFound", "دیتایی با اطلاعات فیلتر شده مدنظر یافت نشد.", 204);
    public static Error ProjectOperationDetailWithProjectOperationIdNotFound = new("NotFound", "ریز متره ای با شناسه عملیات پروژه مدنظر یافت نشد.", 204);
    public static Error ConsumableVolumeProductWithIdNotFound = new("NotFound", "کالای مصرفی ریز متره یافت نشد.", 204);
    public static Error NoHaveProducts = new("NotFound", "ریزمتر انتخابی کالای تایید شده ای ندارد.", 204);
    public static Error ContractorServiceWithIdNotFound = new("NotFound", "خدمت ریز متره یافت نشد.", 422);
    public static Error ContractorServiceIsNotActive = new("NotFound", "خدمت انتخابی ریز متره فعال نمیباشد.", 422);
    public static Error ContractorServiceVolumeInValide = new("NotFound", "حجم خدمت شما بیش از حد مجاز است", 422);
    public static Error ConsumableVolumeMachineryWithIdNotFound = new("NotFound", "ماشین آلات ریز متره یافت نشد.", 204);
    public static Error ConsumableVolumeExpertWithIdNotFound = new("NotFound", "متخصص ریزه متره یافت نشد.", 204);
    public static Error ConsumableVolumeExpertWithFilterDataNotFound = new("NotFound", "تخصصی با فیلتر مدنظر یافت نشد.", 204);
    public static Error ContractorServiceNotFound = new("NotFound", "ریزمتره مد نظر خدمتی ندارد.", 204);
    public static Error ContractorNotFound = new("NotFound", "خدمات این ریزمتره پیمانکاری ندارند.", 204);
    public static Error ProductWithIdNotFound = new("NotFound", "محصول یافت نشد.", 204);
    public static Error HistoryWithIdNotFound = new("NotFound", "تاریخچه با این شناسه یافت نشد.", 204);
    public static Error DailyProjectOperationContractorsWithFilterNotFound = new("NotFound", "هیچ پیمانکاری با اطلاعات ارسالی یافت نشد.", 204);
    public static Error CreatorIdsAreEmpty = new("InvalidArguments", "شناسه های ثبت کننده ها خالی است", 422);

    public static Error InValidUpdateDatetime = new("InvalidArguments", "تاریخ اصلاح باید با تاریخ ایجاد کارکرد روزانه یکسان باشد.", 422);
    public static Error InValidStatus = new("InvalidArguments", "وضعیت نامعتبر است.", 422);
    public static Error InValidStartDate = new("InvalidArguments", "از تاریخ نامعتبر است.", 422);
    public static Error InValidEndDate = new("InvalidArguments", "تا تاریخ نامعتبر است.", 422);
    public static Error EndDateValidate = new("InvalidArguments", "برای تاریخ جاری به بعد نمی‌توان کارکرد روزانه ثبت کرد.", 422);
    public static Error StartDateBiggerThanEndDate = new("InvalidArguments", "تاریخ شروع از تاریخ پایان بزرگتر است،لطفا ابتدا تاریخ شروع و سپس تاریخ پایان را انتخاب کنید.", 422);
    public static Error InValidLength = new("InvalidArguments", "طول نامعتبر است.", 422);
    public static Error InValidWidth = new("InvalidArguments", "عرض نامعتبر است.", 422);
    public static Error InValidHeight = new("InvalidArguments", "ارتفاع نامعتبر است.", 422);
    public static Error InValidWeight = new("InvalidArguments", "وزن نامعتبر است.", 422);
    public static Error InValidNumber = new("InvalidArguments", "تعداد نامعتبر است.", 422);
    public static Error CostCenterIdNotBeNull = new("InvalidArguments", "شناسه مرکز هزینه خالی است.", 422);
    public static Error ProjectIdNotBeNull = new("InvalidArguments", "شناسه پروژه خالی است.", 422);

    public static Error InValidProjectOperationId = new("InvalidArguments", "شرح عملیات نامعتبر است.", 422);
    public static Error InValidProjectOperationDetailId = new("InvalidArguments", "ریز متره نامعتبر است.", 422);
    public static Error InValidProjectOperationDetailStatus = new("InvalidArguments", "وضعیت نامعتبر است.", 422);
    public static Error InValidDailyProjectOperationId = new("InvalidArguments", "شناسه نامعتبر است.", 422);
    public static Error InvalidTime = new("InvalidArguments", "زمان باید در فرمت 00:00 ارسال گردد.", 422);
    public static Error InvalidUnitNumber = new("InvalidArguments", "برای واحد های روزانه و سرویسی باید تعداد وارد گردد و خالی نباشد.", 422);
}
