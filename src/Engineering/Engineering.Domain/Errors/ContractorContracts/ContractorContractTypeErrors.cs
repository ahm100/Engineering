
namespace Engineering.Domain.Errors;

public static class ContractorContractTypeErrors
{
    public static Error TypeNameIsDuplicate = new("Duplicate", "نام نوع قرارداد پیمانکار تکراری است.", 409);
    public static Error TypeCodeIsDuplicate = new("Duplicate", "کد نوع قرارداد پیمانکار تکراری است.", 409);

    public static Error IdIsEmpty = new("InvalidArguments", "شناسه خالی است.", 422);
    public static Error ContractorContractTypeCodeIsEmpty = new("InvalidArguments", "کد خالی است.", 422);
    public static Error ContractorContractTypeNameIsEmpty = new("InvalidArguments", "نام خالی است.", 422);
    public static Error IsActiveIsEmpty = new("InvalidArguments", "وضعیت خالی است.", 422);
    public static Error ContractorContractTypeWithIdNotFound = new("NotFound", "هیچ نوع قرارداد پیمانکار ای با این شناسه یافت نشد.", 404);
    public static Error ContractorContractTypeWithCodeNotFound = new("NotFound", "هیچ نوع قرارداد پیمانکار ای با این کد یافت نشد.", 404);
    public static Error ContractorContractTypeWithNameNotFound = new("NotFound", "هیچ نوع قرارداد پیمانکار ای با این عنوان یافت نشد.", 404);
    public static Error ContractorContractTypesNotFound = new("NotFound", "هیچ نوع قرارداد پیمانکار ای با این اطلاعات یافت نشد.", 404);
    public static Error IsInactive = new("InvalidArguments", "وضعیت نوع قرارداد پیمانکار غیرفعال میباشد.", 422);
    public static Error IsActive = new("InvalidArguments", "وضعیت نوع قرارداد پیمانکار فعال میباشد.", 422);
    public static Error IsDeleted = new("NotFound", "این نوع قرارداد پیمانکار حذف شده است.", 404);
    public static Error CanNotDeleteForContractorContract = new("InvalidArguments", "این نوع قرارداد پیمانکار دارای قرارداد میباشد و نمیتوانید آن را حذف کنید.", 422);
}
