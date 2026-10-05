
namespace Engineering.Domain.Errors;

public static class OperationLocationErrors
{
    public static Error NameIsDuplicate = new("Duplicate", "نام موقعیت عملیات تکراری است.", 409);
    public static Error CodeIsDuplicate = new("Duplicate", "کد موقعیت عملیات تکراری است.", 409);

    public static Error IsActive = new("InvalidArguments", "وضعیت موقعیت عملیات فعال میباشد.", 422);
    public static Error IsInactive = new("InvalidArguments", "وضعیت موقعیت عملیات غیرفعال میباشد.", 422);

    public static Error OperationLocationCostCenterNotFound = new("NotFound", "مرکزهزینه انتخابی یافت نشد.", 204);
    public static Error OperationLocationWithIdNotFound = new("NotFound", "هیچ موقعیت عملیات ای با این شناسه یافت نشد.", 404);
    public static Error OperationLocationWithCodeNotFound = new("NotFound", "هیچ موقعیت عملیات ای با این کد یافت نشد.", 404);
    public static Error OperationLocationWithNameNotFound = new("NotFound", "هیچ موقعیت عملیات ای با این نام یافت نشد.", 404);
    public static Error FilteredOperationLocationNotFound = new("NotFound", "هیچ موقعیت عملیات ای با این اطلاعات یافت نشد.", 204);
    public static Error OperationLocationChildNotFound = new("NotFound", "موقعیت عملیات انتخابی هیچ زیرشاخه ای ندارد.", 204);
    public static Error IsDeleted = new("NotFound", "این موقعیت عملیات حذف شده است.", 204);
    public static Error CanNotDeleteBecauseOfChildren = new("InvalidArguments", "این موقعیت عملیات به دلیل والدبودن برای موقعیتی دیگر قابل حذف نمیباشد.", 422);
    public static Error CanNotDeleteBecauseOfProjOpDetail = new("InvalidArguments", "این موقعیت عملیات به دلیل دارا بودن ریزمتره قابل حذف نمیباشد.", 422);

    public static Error UnValidIds = new("InvalidArguments", "هیچ شناسه موقعیت عملیاتی وارد نشده است.", 422);
    public static Error UnValidId = new("InvalidArguments", "شناسه موقعیت عملیات نامعتبر است.", 422);
    public static Error IdIsEmpty = new("InvalidArguments", "شناسه موقعیت عملیات نامعتبر است.", 422);
    public static Error UnValidData = new("InvalidArguments", "اطلاعات وارد شده نامعتبر است.", 422);
    public static Error CostCenterIsEmpty = new("InvalidArguments", "مرکزهزینه خالی میباشد.", 422);
    public static Error ParentIdIsEmpty = new("InvalidArguments", "شناسه والد سطح خالی میباشد.", 422);
    public static Error OperationLocationNameIsEmpty = new("InvalidArguments", "نام خالی میباشد.", 422);
    public static Error PublicNameIsEmpty = new("InvalidArguments", "نام عمومی خالی میباشد.", 422);
    public static Error PrivateNameIsEmpty = new("InvalidArguments", "نام خصوصی خالی میباشد.", 422);
    public static Error OperationLocationLatinNameIsEmpty = new("InvalidArguments", "نام لاتین خالی میباشد.", 422);
    public static Error PrivateCodeIsEmpty = new("InvalidArguments", "کد خصوصی خالی میباشد.", 422);
    public static Error PublicCodeIsEmpty = new("InvalidArguments", "کد عمومی خالی میباشد.", 422);
    public static Error PathIsEmpty = new("InvalidArguments", "مسیر خالی میباشد.", 422);
    public static Error PriorityIsEmpty = new("InvalidArguments", "الویت خالی میباشد.", 422);
    public static Error IsActiveIsEmpty = new("InvalidArguments", "وضعیت فعال بودن خالی میباشد.", 422);
    public static Error CanNotUseParent = new("InvalidArguments", "شما نمیتوانین از شناسه آدرس و شناسه والد یکی استفاده کنید", 422);

    public static Error HaveChild = new("InvalidArguments", "موقعیت عملیات انتخابی به دلیل وابستگی اطلاعاتی، قابل تغییر نمیباشد.", 422);

}