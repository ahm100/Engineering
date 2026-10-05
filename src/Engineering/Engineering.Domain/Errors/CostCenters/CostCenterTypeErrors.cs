namespace Engineering.Domain.Errors;

public static class CostCenterTypeErrors
{
    public static Error NotFoundWithId = new("InvalidArguments", "هیچ نوع مرکزهزینه ای با این شناسه وجود ندارد.", 422);

    public static Error CostCenterTypeNameIsEmpty = new("InvalidArguments", "نام نوع مرکزهزینه خالی میباشد!", 422);
    public static Error CostCenterTypeCodeIsEmpty = new("InvalidArguments", "کد نوع مرکزهزینه خالی میباشد!", 422);
    public static Error IdIsEmpty = new("InvalidArguments", "شناسه خالی است!", 422);
    public static Error IsActiveIsEmpty = new("InvalidArguments", "وضعیت خالی است!", 422);

    public static Error CostCenterTypeWithIdNotFound = new("NotFound", "هیچ نوع مرکزهزینه ای با این شناسه یافت نشد.", 404);
    public static Error CostCenterTypeWithCodeNotFound = new("NotFound", "هیچ نوع مرکزهزینه ای با این کد یافت نشد.", 404);
    public static Error CostCenterTypeWithNameNotFound = new("NotFound", "هیچ نوع مرکزهزینه ای با این عنوان یافت نشد.", 404);
    public static Error CostCenterTypesNotFound = new("NotFound", "هیچ نوع مرکزهزینه ای با این اطلاعات یافت نشد.", 404);
    public static Error IsInactive = new("InvalidArguments", "وضعیت نوع مرکزهزینه غیرفعال میباشد.", 422);
    public static Error IsActive = new("InvalidArguments", "وضعیت نوع مرکزهزینه فعال میباشد.", 422);
    public static Error IsDeleted = new("NotFound", "این نوع مرکزهزینه حذف شده است.", 404);
}