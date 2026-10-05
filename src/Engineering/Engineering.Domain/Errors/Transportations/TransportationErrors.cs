
namespace Engineering.Domain.Errors;

public static class TransportationErrors
{
    public static Error NameIsDuplicate = new("Duplicate", "نام ترابری تکراری است.", 409);
    public static Error CodeIsDuplicate = new("Duplicate", "کد ترابری تکراری است.", 409);

    public static Error IsActive = new("InvalidArguments", "وضعیت ترابری فعال میباشد.", 422);
    public static Error IsInactive = new("InvalidArguments", "وضعیت ترابری غیرفعال میباشد.", 422);
    public static Error CanNottDelete = new("InvalidArguments", "این ترابری به دلیل داشتن وابستگی اطلاعاتی قابل حذف نمیباشد.", 422);

    public static Error IdIsEmpty = new("InvalidArguments", "شناسه ترابری خالی است.", 422);
    public static Error NameIsEmpty = new("InvalidArguments", "نام ترابری خالی است.", 422);
    public static Error CodeIsEmpty = new("InvalidArguments", "کد ترابری خالی است.", 422);
    public static Error IsPassengerIsEmpty = new("InvalidArguments", "نوع مسافری ترابری خالی است.", 422);
    public static Error IsActiveIsEmpty = new("InvalidArguments", "وضعیت ترابری خالی است.", 422);
    public static Error TransportationWithIdNotFound = new("NotFound", "هیچ ترابری ای با این شناسه یافت نشد.", 404);
    public static Error TransportationWithCodeNotFound = new("NotFound", "هیچ ترابری ای با این کد یافت نشد.", 404);
    public static Error TransportationWithNameNotFound = new("NotFound", "هیچ ترابری ای با این نام یافت نشد.", 404);
    public static Error FilteredTransportationNotFound = new("NotFound", "هیچ ترابری ای با این اطلاعات یافت نشد.", 204);
    public static Error TransportationChildNotFound = new("NotFound", "ترابری انتخابی هیچ زیرشاخه ای ندارد.", 204);
    public static Error IsDeleted = new("NotFound", "این ترابری حذف شده است.", 204);
    public static Error TransportationTypeIsEmpty = new("NotFound", "نوع ترابری معتبر نیست", 204);

    public static Error UnValidId = new("InvalidArguments", "شناسه ترابری نامعتبر است.", 422);
    public static Error UnValidData = new("InvalidArguments", "اطلاعات وارد شده نامعتبر است.", 422);
    public static Error IsLockData = new("InvalidArguments", "این نوع ترابری  بدلیل استفاده در  بخشی دیگر قابل حذف، ویرایش، فعال یا غیرفعالسازی نمیباشد..", 422);

    public static Error HaveChild = new("InvalidArguments", "ترابری انتخابی به دلیل وابستگی اطلاعاتی، قابل تغییر نمیباشد.", 422);

}