namespace Engineering.Domain.Errors;

public static class CabinTypeErrors
{
    public static Error TypeNameIsDuplicate = new("Duplicate", "نام نوع اتاق تکراری است.", 409);
    public static Error TypeCodeIsDuplicate = new("Duplicate", "کد نوع اتاق تکراری است.", 409);

    public static Error IdIsEmpty = new("InvalidArguments", "شناسه خالی است.", 422);
    public static Error CabinTypeCodeIsEmpty = new("InvalidArguments", "کد خالی است.", 422);
    public static Error CabinTypeNameIsEmpty = new("InvalidArguments", "نام خالی است.", 422);
    public static Error IsActiveIsEmpty = new("InvalidArguments", "وضعیت خالی است.", 422);
    public static Error CabinTypeWithIdNotFound = new("NotFound", "هیچ نوع اتاقی با این شناسه یافت نشد.", 404);
    public static Error CabinTypeWithCodeNotFound = new("NotFound", "هیچ نوع اتاقی با این کد یافت نشد.", 404);
    public static Error CabinTypeWithNameNotFound = new("NotFound", "هیچ نوع اتاقی با این عنوان یافت نشد.", 404);
    public static Error CabinTypesNotFound = new("NotFound", "هیچ نوع اتاقی با این اطلاعات یافت نشد.", 404);
    public static Error IsInactive = new("InvalidArguments", "وضعیت نوع اتاق غیرفعال میباشد.", 422);
    public static Error IsActive = new("InvalidArguments", "وضعیت نوع اتاق فعال میباشد.", 422);
    public static Error IsDeleted = new("NotFound", "این نوع اتاق حذف شده است.", 404);
    public static Error CanNotDeleteForCabin = new("InvalidArguments", "این نوع اتاق دارای قرارداد میباشد و نمیتوانید آن را حذف کنید.", 422);
}