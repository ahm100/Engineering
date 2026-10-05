
namespace Engineering.Domain.Errors;

public static class ProjectOperationDetailDeductionErrors
{
    public static Error ProjectOperationDetailIdIsEmpty = new("InvalidArguments", "شناسه ریزمتره خالی است.", 422);
    public static Error IdIsEmpty = new("InvalidArguments", "شناسه خالی است.", 422);
    public static Error IdsIsEmpty = new("InvalidArguments", "شناسه ها خالی هستند.", 422);

    public static Error LengthMustGreaterZiro = new("InvalidArguments", "طول انتخابی باید بزرگرتر صفر باشد.", 422);
    public static Error WidthMustGreaterZiro = new("InvalidArguments", "عرض انتخابی باید بزرگرتر صفر باشد.", 422);
    public static Error HeightMustGreaterZiro = new("InvalidArguments", "ارتفاع انتخابی باید بزرگرتر صفر باشد.", 422);
    public static Error WeightMustGreaterZiro = new("InvalidArguments", "وزن انتخابی باید بزرگرتر صفر باشد.", 422);
    public static Error NumberMustGreaterZiro = new("InvalidArguments", "تعداد انتخابی باید بزرگرتر صفر باشد.", 422);
    public static Error LengthIsEmpty = new("InvalidArguments", "طول خالی است.", 422);
    public static Error WidthIsEmpty = new("InvalidArguments", "عرض خالی است.", 422);
    public static Error HeightIsEmpty = new("InvalidArguments", "ارتفاع خالی است.", 422);
    public static Error WeightIsEmpty = new("InvalidArguments", "وزن خالی است.", 422);
    public static Error NumberIsEmpty = new("InvalidArguments", "تعداد خالی است.", 422);

    public static Error CanNotDelete = new("InvalidArguments", "شما نمیتوانید این کسورات را حذف کنید.", 422);
    public static Error DeductionsNotFound = new("NotFound", "هیچ داده ای با این اطلاعات یافت نشد.", 204);
    public static Error DeductionWithIdNotFound = new("NotFound", "کسورات ریزمتره با این شناسه یافت نشد.", 422);
    public static Error DeductionWithDetailIdNotFound = new("NotFound", "کسوراتی با این شناسه ریزمتره یافت نشد.", 422);
    public static Error IsDeleted = new("NotFound", "کسورات ریزمتره حذف شده است.", 204);
    public static Error IsActive = new("InvalidArguments", "وضعیت فعال میباشد.", 422);
    public static Error IsInactive = new("InvalidArguments", "وضعیت غیرفعال میباشد.", 422);
}