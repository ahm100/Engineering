
namespace Engineering.Domain.Errors;

public static class ContractorEmployeeErrors
{
    public static Error ContractorEmployeeIsDuplicate = new("Duplicate", "پرسنل پیمانکار تکراری است.", 409);

    public static Error InValidId = new("InvalidArguments", "شناسه نامعتبر است.", 422);
    public static Error InValidEmployeeId = new("InvalidArguments", "پرسنل نامعتبر است.", 422);
    public static Error InValidContractorId = new("InvalidArguments", "پیمانکار نامعتبر است.", 422);

    public static Error IsDuplicatedOperation = new("NotFound", "عملیات مورد نظر تکراری می باشد", 409);
    public static Error IsDuplicatedActive = new("NotFound", "پرسنل پیمانکار برای پیمانکار دیگری فعال می‌باشد", 409);

    public static Error IsDeleted = new("NotFound", "پرسنل پیمانکار حذف شده است.", 204);

    public static Error ContractorEmployeeIdNotFound = new("NotFound", "هیچ پرسنل پیمانکار با این شناسه یافت نشد.", 404);

}
