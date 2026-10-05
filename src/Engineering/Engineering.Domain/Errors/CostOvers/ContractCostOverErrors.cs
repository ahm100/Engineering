
namespace Engineering.Domain.Errors;

public static class ContractCostOverErrors
{
    public static Error NameIsDuplicate = new("Duplicate", "نام ضریب بالا سری تکراری است.", 409);
    public static Error CodeIsDuplicate = new("Duplicate", "کد ضریب بالا سری تکراری است.", 409);

    public static Error IsActive = new("InvalidArguments", "وضعیت ضریب بالا سری فعال میباشد.", 422);
    public static Error IsInactive = new("InvalidArguments", "وضعیت ضریب بالا سری غیرفعال میباشد.", 422);

    public static Error ContractCostOverCostOverNotFound = new("NotFound", "هیچ ضریب بالا سری ای یافت نشد.", 204);
    public static Error RelatedContractCostOverNotFound = new("NotFound", "ضرایب بالا سری وابسته یافت نشد.", 204);
    public static Error DuplicateRelatedContractCostOver = new("NotFound", "تاثیرگذاری تکراری ثبت شده است لطفا درخواست را اصلاح کنین.", 204);
    public static Error ContractCostOverWithIdNotFound = new("NotFound", "هیچ ضریب بالا سری ای با این شناسه یافت نشد.", 404);
    public static Error RelatedNotFound = new("NotFound", "تاثیرگذاری انتخاب شده در لیست ضرایب وجود ندارد.", 404);
    public static Error ContractCostOverWithCodeNotFound = new("NotFound", "هیچ ضریب بالا سری ای با این کد یافت نشد.", 404);
    public static Error ContractCostOverWithNameNotFound = new("NotFound", "هیچ ضریب بالا سری ای با این نام یافت نشد.", 404);
    public static Error FilteredContractCostOverNotFound = new("NotFound", "هیچ ضریب بالا سری ای با این اطلاعات یافت نشد.", 204);
    public static Error ContractCostOverChildNotFound = new("NotFound", "ضریب بالا سری انتخاب شده زیرشاخه ای ندارد.", 204);
    public static Error IsDeleted = new("NotFound", "این ضریب بالا سری حذف شده است.", 204);
    public static Error CanNotDelete = new("NotFound", "به دلیل گذاشتن تاثیر برروری دیگر ضرایب قابل حذف نمیباشد.", 204);

    public static Error UnValidId = new("InvalidArguments", "شناسه ضریب بالا سری نامعتبر است.", 422);
    public static Error UnValidData = new("InvalidArguments", "اطلاعات وارد شده نامعتبر است.", 422);

    public static Error HaveChild = new("InvalidArguments", "ضریب بالا سری مورد نظر بدلیل وابستگی اطلاعاتی قابل تغییر نیست.", 422);

    public static Error EmployerContractIsEmpty = new("InvalidArguments", "قرارداد کارفرما خالی است!", 422);
    public static Error CostOverIsEmpty = new("InvalidArguments", "سربار خالی است!", 422);
    public static Error OrderIsEmpty = new("InvalidArguments", "ترتیب خالی است!", 422);
    public static Error PerecentIsEmpty = new("InvalidArguments", "درصد خالی است!", 422);
    public static Error IdIsEmpty = new("InvalidArguments", "شناسه خالی است!", 422);
}