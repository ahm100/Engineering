
namespace Engineering.Domain.Errors;

public static class ConsumableVolumeProductErrors
{
    public static Error UnValidId = new("InvalidArguments", "شناسه برآورد نامعتبر است.", 422);
    public static Error CostCenterIdIsEmpty = new("InvalidArguments", "شناسه مرکزهزینه نامعتبر است.", 422);
    public static Error DateTimeNotValid = new("InvalidArguments", "تاریخ پایان باید بزرگتر از تاریخ شروع باشد.", 422);
    public static Error AvailableId = new("InvalidArguments", "اطلاعات این کار وجود دارد.", 422);
    public static Error UnValidImplementations = new("InvalidArguments", "شناسه ای از مسئولین اجرایی نامعتبر است.", 422);
    public static Error UnValidTechnicals = new("InvalidArguments", "شناسه ای از مسئولین فنی نامعتبر است.", 422);
    public static Error UnValidContractors = new("InvalidArguments", "شناسه ای از پیمانکار نامعتبر است.", 422);
    public static Error UnValidServiceInfos = new("InvalidArguments", "شناسه ای از خدمات نامعتبر است.", 422);
    public static Error UnValidExperts = new("InvalidArguments", "شناسه ای از متخصص نامعتبر است.", 422);
    public static Error UnValidMachineries = new("InvalidArguments", "شناسه ای از ماشین آلات و ابزار نامعتبر است.", 422);
    public static Error UnValidProducts = new("InvalidArguments", "شناسه ای از کالا نامعتبر است.", 422);
    public static Error UnValidForSupplies = new("InvalidArguments", "حجم این کالا بدلیل داشتن درخواست تامین قابل تغییر نمیباشد.", 422);
    public static Error UnValidCategories = new("InvalidArguments", "شناسه ای از دسته بندی نامعتبر است.", 422);
    public static Error UnValidMeasureunits = new("InvalidArguments", "شناسه ای از واحد اندازگیری نامعتبر است.", 422);
    public static Error DataIsStandard = new("NotAccess", "کالا انتخابی، بدلیل استاندارد بودن قابل حذف نمیباشد.", 422);
    public static Error NoHaveProducts = new("InvalidArguments", "هیچ کالایی وجود ندارد.", 422);
    public static Error HaveRequestGoodsSupplyDetails = new("InvalidArguments", "شما درخواست تامین کالا دارید و نمیتوانین این کالا را پاک کنین.", 422);

    public static Error ProjectOperationDetailIdIsEmpty = new("InvalidArguments", "شناسه ریزمتره نامعتبر است.", 422);
    public static Error ProductGroupIdIsEmptyIsEmpty = new("InvalidArguments", "شناسه گروه کالا نامعتبر است.", 422);
    public static Error ProjectOperationIdIsEmpty = new("InvalidArguments", "شناسه شرح عملیات پروژه نامعتبر است.", 422);
    public static Error FinalValueIsEmpty = new("InvalidArguments", "زمان مصرفی خالی است!", 422);
    public static Error NumberIsEmpty = new("InvalidArguments", "تعداد خالی است!", 422);
    public static Error IdIsEmpty = new("InvalidArguments", "شناسه خالی است!", 422);
    public static Error IsStandardIsEmpty = new("InvalidArguments", "وضعیت استاندارد خالی میباشد!", 422);
    public static Error UnusedPercentageIsEmpty = new("InvalidArguments", "درصد مصرف خالی است!", 422);

    public static Error ProjectOperationDetailWithIdNotFound = new("NotFound", "هیچ برآورد ای با این شناسه یافت نشد.", 404);
    public static Error ProjectOperationDetailProductWithIdNotFound = new("NotFound", "هیچگونه احجام مصرفی کالایی برای این ریزمتره یافت نشد.", 204);
    public static Error ProjectChildNotFound = new("NotFound", "ریزمتره انتخابی زیر شاخه ای ندارد.", 204);
    public static Error DependencyWithIdNotFound = new("NotFound", "هیج وابستگی ای یافت نشد.", 204);
    public static Error CanNottDelete = new("InvalidArguments", "این برآورد به دلیل داشتن وابستگی اطلاعاتی قابل حذف نمیباشد.", 422);
    public static Error CanNotDelete = new("InvalidArguments", "شما نمیتوانین این کالا را بدون شناسه را حذف کنین.", 422);

    public static Error UnValidType = new("InvalidArguments", "نوع انتخابی نامعتبر است.", 422);
}