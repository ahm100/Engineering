
namespace Engineering.Domain.Errors;

public static class CostCenterErrors
{
    public static Error CanNotDelete = new("InvalidArguments", "این دیتا به دلیل داشتن وابستگی اطلاعاتی قابل حذف نمیباشد.", 422);
    public static Error CanNotDeleteBecauseOfProject = new("InvalidArguments", "به دلیل داشتن پروژه، این مرکزهزینه قابل حذف نمیباشد.", 422);
    public static Error CanNotDeleteBecuseOfCostCenter = new("InvalidArguments", "به دلیل داشتن مرکزهزینه، این  نوع مرکزهزینه قابل حذف نمیباشد.", 422);
    public static Error NameIsDuplicate = new("Duplicate", "نام مرکزهزینه تکراری است.", 409);
    public static Error ProjectNotInCostCenter = new("Duplicate", "پروژه انتخابی در مرکزهزینه انتخابی وجود ندارد.", 409);
    public static Error TypeNameIsDuplicate = new("Duplicate", "نام نوع مرکزهزینه تکراری است.", 409);
    public static Error TypeCodeIsDuplicate = new("Duplicate", "کد نوع مرکزهزینه تکراری است.", 409);
    public static Error CodeIsDuplicate = new("Duplicate", "کد مرکزهزینه تکراری است.", 409);
    public static Error ChildIsDuplicate = new("Duplicate", "کاربر انتخابی از قبل موجود است.", 409);
    public static Error WarehouseIdIsEmpty = new("Duplicate", "شناسه انبار خالی است", 409);

    public static Error IsActive = new("InvalidArguments", "وضعیت مرکزهزینه فعال میباشد.", 422);
    public static Error IsInactive = new("InvalidArguments", "وضعیت مرکزهزینه غیرفعال میباشد.", 422);
    public static Error CityIdNotValid = new("InvalidArguments", "شناسه شهر درست نمیباشد.", 422);
    public static Error UnValidWarehouses = new("InvalidArguments", "شناسه انبار درست نمیباشد.", 422);
    public static Error UnValidInformedUsers = new("InvalidArguments", "شناسه ای از کاربران مطلع درست نمیباشد.", 422);
    public static Error UnValidAuthorizedRoles = new("InvalidArguments", "شناسه ای از نقش های مجاز درست نمیباشد.", 422);
    public static Error UnValidAuthorizedUsers = new("InvalidArguments", "شناسه ای از کاربر های مجاز درست نمیباشد.", 422);
    public static Error CostCenterWarehousesHaveMoreDefault = new("InvalidArguments", "تعداد انبار های پیش فرض بیش از یک مورد هست.", 422);
    public static Error CostCenterWarehousesHaveNoDefault = new("InvalidArguments", "هیج انبار پیش فرضی انتخاب نشده است.", 422);
    public static Error CostCenterDefaultWarehouseCantDelete = new("InvalidArguments", "شما نمیتوانین انبار پیش فرض را حذف کنید.", 422);
    public static Error HaveChild = new("InvalidArguments", "مرکزهزینه بدلیل وابستگی اطلاعاتی قابل تغییر نیست.", 422);

    public static Error CostCenterWithIdNotFound = new("NotFound", "هیچ مرکزهزینه ای با این شناسه یافت نشد.", 404);
    public static Error CostCenterWithCodesNotFound = new("NotFound", "هیچ مرکزهزینه ای با این کدها یافت نشد.", 204);
    public static Error CostCenterWithCodeNotFound = new("NotFound", "هیچ مرکزهزینه ای با این کد یافت نشد.", 404);
    public static Error CostCenterWithNameNotFound = new("NotFound", "هیچ مرکزهزینه ای با این نام یافت نشد.", 404);
    public static Error IsDeleted = new("NotFound", "این مرکزهزینه حذف شده است.", 204);
    public static Error FilteredCostCenterNotFound = new("NotFound", "هیچ مرکزهزینه ای با این اطلاعات یافت نشد.", 204);
    public static Error CostCenterChildNotFound = new("NotFound", "مرکزهزینه انتخابی زیرشاخه ای ندارد.", 204);

    public static Error UnValidId = new("InvalidArguments", "شناسه مرکزهزینه نامعتبر است.", 422);
    public static Error IdIsEmpty = new("InvalidArguments", "شناسه مرکزهزینه خالی میباشد.", 422);
    public static Error TypeIdIsEmpty = new("InvalidArguments", "شناسه نوع مرکزهزینه خالی میباشد.", 422);
    public static Error CostCenterTypeIdIsEmpty = new("InvalidArguments", "شناسه نوع مرکزهزینه خالی میباشد.", 422);
    public static Error CostCenterNameIsEmpty = new("InvalidArguments", " نام مرکزهزینه خالی میباشد.", 422);
    public static Error CostCenterCodeIsEmpty = new("InvalidArguments", " کد مرکزهزینه خالی میباشد.", 422);
    public static Error CostCenterCodesIsEmpty = new("InvalidArguments", " کد های مرکزهزینه خالی میباشد.", 422);
    public static Error CityIdIsEmpty = new("InvalidArguments", "شناسه شهر خالی میباشد.", 422);
    public static Error AddressIsEmpty = new("InvalidArguments", "آدرس خالی میباشد.", 422);
    public static Error NoOperationDaysIsEmpty = new("InvalidArguments", "روز بدون کار خالی میباشد.", 422);
    public static Error WeatherStateIsEmpty = new("InvalidArguments", "ثبت وضعیت آبوهوایی خالی میباشد.", 422);
    public static Error IsActiveIsEmpty = new("InvalidArguments", "وضعیت خالی میباشد.", 422);
    public static Error RoleIdIsEmpty = new("InvalidArguments", "شناسه نقش خالی میباشد.", 422);
    public static Error UserIdIsEmpty = new("InvalidArguments", "شناسه کاربر مجاز خالی میباشد.", 422);
    public static Error EmployerIdIsEmpty = new("InvalidArguments", "شناسه کاربر خالی میباشد.", 422);
    public static Error ContractorIdIsEmpty = new("InvalidArguments", "شناسه پیمانکار خالی میباشد.", 422);
    public static Error ProjectManagerIdIsEmpty = new("InvalidArguments", "شناسه مدیر پروژه خالی میباشد.", 422);
    public static Error FilterDataIsEmpty = new("InvalidArguments", "شناسه کاربر خالی میباشد.", 422);


}