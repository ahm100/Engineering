
namespace Engineering.Domain.Errors;

public static class ConsumableVolumeMachineryErrors
{
    public static Error UnValidId = new("InvalidArguments", "شناسه برآورد نامعتبر است.", 422);
    public static Error DateTimeNotValid = new("InvalidArguments", "تاریخ پایان باید بزرگتر از تاریخ شروع باشد.", 422);
    public static Error AvailableId = new("InvalidArguments", "اطلاعات این ماشین آلات وجود دارد.", 422);
    public static Error UnValidImplementations = new("InvalidArguments", "شناسه ای از مسئولین اجرایی نامعتبر است.", 422);
    public static Error UnValidTechnicals = new("InvalidArguments", "شناسه ای از مسئولین فنی نامعتبر است.", 422);
    public static Error UnValidContractors = new("InvalidArguments", "شناسه ای از پیمانکار نامعتبر است.", 422);
    public static Error UnValidServiceInfos = new("InvalidArguments", "شناسه ای از خدمات نامعتبر است.", 422);
    public static Error UnValidMachinerys = new("InvalidArguments", "شناسه ای از ماشین آلات نامعتبر است.", 422);
    public static Error UnValidMachineries = new("InvalidArguments", "شناسه ای از ماشین آلات و ابزار نامعتبر است.", 422);
    public static Error UnValidProducts = new("InvalidArguments", "شناسه ای از کالا نامعتبر است.", 422);
    public static Error UnValidMeasureunits = new("InvalidArguments", "شناسه ای از واحد اندازگیری نامعتبر است.", 422);
    public static Error DataIsStandard = new("NotAccess", "ماشین آلات انتخابی، بدلیل استاندارد بودن قابل حذف نمیباشد.", 422);
    public static Error NoHaveMachineries = new("InvalidArguments", "هیچ ماشین آلاتی یافت نشد.", 422);

    public static Error ProjectOperationDetailIdIsEmpty = new("InvalidArguments", "شناسه ریزمتره نامعتبر است.", 422);
    public static Error ProjectOperationIdIsEmpty = new("InvalidArguments", "شناسه شرح عملیات پروژه نامعتبر است.", 422);
    public static Error ProjectIdIsEmpty = new("InvalidArguments", "شناسه پروژه نامعتبر است.", 422);
    public static Error CostCenterIdIsEmpty = new("InvalidArguments", "شناسه مرکزهزینه نامعتبر است.", 422);
    public static Error MachineryIdIsEmpty = new("InvalidArguments", "شناسه ماشین آلات نامعتبر است.", 422);
    public static Error MachineryIsEmpty = new("InvalidArguments", "شناسه ای از ماشین آلات نامعتبر است.", 422);
    public static Error FinalValueIsEmpty = new("InvalidArguments", "زمان مصرفی خالی است!", 422);
    public static Error NumberIsEmpty = new("InvalidArguments", "تعداد خالی است!", 422);
    public static Error IdIsEmpty = new("InvalidArguments", "شناسه خالی است!", 422);
    public static Error IsStandardIsEmpty = new("InvalidArguments", "وضعیت استاندارد خالی میباشد!", 422);
    public static Error UnusedPercentageIsEmpty = new("InvalidArguments", "درصد مصرف خالی است!", 422);

    public static Error ProjectOperationDetailWithIdNotFound = new("NotFound", "هیچ برآورد ای با این شناسه یافت نشد.", 404);
    public static Error ProjectChildNotFound = new("NotFound", "ریزمتره انتخابی زیر شاخه ای ندارد.", 204);
    public static Error DataNotFound = new("NotFound", "با این اطلاعات دیتایی یافت نشد.", 204);
    public static Error DependencyWithIdNotFound = new("NotFound", "هیج وابستگی ای یافت نشد.", 204);
    public static Error CanNottDelete = new("InvalidArguments", "این برآورد به دلیل داشتن وابستگی اطلاعاتی قابل حذف نمیباشد.", 422);
    public static Error CanNotDelete = new("InvalidArguments", "شما نمیتوانین این ماشین آلات را بدون شناسه را حذف کنین.", 422);
}