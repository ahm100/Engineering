
namespace Engineering.Domain.Errors;

public static class ContractorServicesErrors
{
    public static Error ContractorServiceIsDuplicate = new("Duplicate", "خدمت پیمانکار تکراری است.", 409);

    public static Error InValidId = new("InvalidArguments", "شناسه نامعتبر است.", 422);
    public static Error InValidServiceInfoId = new("InvalidArguments", "خدمت نامعتبر است.", 422);
    public static Error InValidContractorId = new("InvalidArguments", "پیمانکار نامعتبر است.", 422);

    public static Error ContractorIdIsEmpty = new("InvalidArguments", "شناسه پیمانکار خالی می باشد", 422);
    public static Error SkillIdIsEmpty = new("InvalidArguments", "شناسه تخصص خالی می باشد", 422);
    public static Error ServiceInfoIdIsEmpty = new("InvalidArguments", "شناسه خدمت خالی می باشد", 422);
    public static Error IdIsEmpty = new("InvalidArguments", "شناسه اجباری می باشد.", 422);
    public static Error ProjectIdIsEmpty = new("InvalidArguments", "شناسه پروژه اجباری می باشد.", 422);
    public static Error InvalidPageIndex = new("InvalidArguments", "شماره صفحه باید بزرگتر از 0  و خالی نباشد.", 422);
    public static Error InvalidPageSize = new("InvalidArguments", "اندازه صفحه باید بزرگتر از 0  و خالی نباشد.", 422);
    public static Error ContractorNotFound = new("InvalidArguments", "اطلاعات پیمانکار یافت نشد.", 422);

    public static Error IsDeleted = new("NotFound", "خدمت پیمانکار حذف شده است.", 204);

    public static Error ContractorServiceIdNotFound = new("NotFound", "هیچ خدمت پیمانکار با این شناسه یافت نشد.", 404);

}
