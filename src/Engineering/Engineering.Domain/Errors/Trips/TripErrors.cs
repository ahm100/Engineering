
namespace Engineering.Domain.Errors;

public static class TripErrors
{
    public static Error NameIsDuplicate = new("Duplicate", "نام سفر تکراری است.", 409);
    public static Error CodeIsDuplicate = new("Duplicate", "کد سفر تکراری است.", 409);

    public static Error IsActive = new("InvalidArguments", "وضعیت سفر فعال میباشد.", 422);
    public static Error IsInactive = new("InvalidArguments", "وضعیت سفر غیرفعال میباشد.", 422);
    public static Error CanNottDelete = new("InvalidArguments", "این سفر به دلیل داشتن وابستگی اطلاعاتی قابل حذف نمیباشد.", 422);

    public static Error IdIsEmpty = new("InvalidArguments", "شناسه سفر خالی است.", 422);
    public static Error NameIsEmpty = new("InvalidArguments", "نام سفر خالی است.", 422);
    public static Error CodeIsEmpty = new("InvalidArguments", "کد سفر خالی است.", 422);
    public static Error CodesIsEmpty = new("InvalidArguments", "کد های سفر خالی است.", 422);
    public static Error IsActiveIsEmpty = new("InvalidArguments", "وضعیت سفر خالی است.", 422);
    public static Error TripWithIdNotFound = new("NotFound", "هیچ سفر ای با این شناسه یافت نشد.", 404);
    public static Error TripWithCodeNotFound = new("NotFound", "هیچ سفر ای با این کد یافت نشد.", 404);
    public static Error TripWithCodesNotFound = new("NotFound", "هیچ سفر ای با این کد ها یافت نشد.", 204);
    public static Error TripWithNameNotFound = new("NotFound", "هیچ سفر ای با این نام یافت نشد.", 404);
    public static Error FilteredTripNotFound = new("NotFound", "هیچ نوع سفری با این اطلاعات یافت نشد.", 204);
    public static Error TripChildNotFound = new("NotFound", "سفر انتخابی هیچ زیرشاخه ای ندارد.", 204);
    public static Error IsDeleted = new("NotFound", "این سفر حذف شده است.", 204);

    public static Error UnValidId = new("InvalidArguments", "شناسه سفر نامعتبر است.", 422);
    public static Error UnValidData = new("InvalidArguments", "اطلاعات وارد شده نامعتبر است.", 422);
    public static Error IsLockData = new("InvalidArguments", "این نوع سفر بدلیل استفاده در  بخشی دیگر قابل حذف، ویرایش، فعال یا غیرفعالسازی نمیباشد.", 422);

    public static Error HaveChild = new("InvalidArguments", "سفر انتخابی به دلیل وابستگی اطلاعاتی، قابل تغییر نمیباشد.", 422);
    public static Error IsDeletedForTransportationRequest = new("NotAccess", "سفر انتخابی به دلیل دارا بودن درخواست ترابری قابل حذف نمیباشد.", 422);

}