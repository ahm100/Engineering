
namespace Engineering.Domain.Errors;

public static class ProjectOperationDetailInspectionErrors
{
    public static Error InspectionDocumentWithIdNotFound = new("NotFound", "هیچ پیوست بازرسی با این شناسه یافت نشد.", 404);
    public static Error InspectionWithIdNotFound = new("NotFound", "هیچ بازرسی با این شناسه یافت نشد.", 404);
    public static Error FilteredInspectionNotFound = new("NotFound", "هیچ بازرسی با این اطلاعات یافت نشد.", 204);
    public static Error IsDeleted = new("NotFound", "این بازرسی حذف شده است.", 204);

    public static Error InspectionIsEmpty = new("InvalidArguments", " بازرسی  خالی است.", 422);
    public static Error CreatorIsEmpty = new("InvalidArguments", "درخواست دهنده خالی است.", 422);
    public static Error CostCenterIsEmpty = new("InvalidArguments", "مرکز هزینه خالی است.", 422);
    public static Error CostCenterIdIsEmpty = new("InvalidArguments", "شناسه مرکز هزینه خالی است.", 422);
    public static Error ProjectIsEmpty = new("InvalidArguments", "پروژه خالی است.", 422);
    public static Error ProjectIdIsEmpty = new("InvalidArguments", "شناسه پروژه خالی است.", 422);
    public static Error ProjectOperationIsEmpty = new("InvalidArguments", "شرح عملیات پروژه خالی است.", 422);
    public static Error ProjectOperationIdIsEmpty = new("InvalidArguments", "شناسه شرح عملیات پروژه خالی است.", 422);
    public static Error ProjectOperationDetailIsEmpty = new("InvalidArguments", "ریزمتره خالی است.", 422);
    public static Error ProjectOperationDetailIdIsEmpty = new("InvalidArguments", "شناسه ریزمتره خالی است.", 422);
    public static Error IdIsEmpty = new("InvalidArguments", "شناسه بازرسی خالی است.", 422);
    public static Error DocumentIdIsEmpty = new("InvalidArguments", "شناسه پیوست بازرسی خالی است.", 422);
    public static Error UrlIsEmpty = new("InvalidArguments", "پیوست خالی است.", 422);
    public static Error UnValidId = new("InvalidArguments", "اطلاعات وارد شده نامعتبر است.", 422);
    public static Error UnValidRequester = new("InvalidArguments", "درخواست دهنده نامعتبر است.", 422);
    public static Error LengthMustGreaterZiro = new("InvalidArguments", "طول وارد شده باید بزرگرتر صفر باشد.", 422);
    public static Error WidthMustGreaterZiro = new("InvalidArguments", "عرض وارد شده باید بزرگرتر صفر باشد.", 422);
    public static Error HeightMustGreaterZiro = new("InvalidArguments", "ارتفاع وارد شده باید بزرگرتر صفر باشد.", 422);
    public static Error WeightMustGreaterZiro = new("InvalidArguments", "وزن وارد شده باید بزرگرتر صفر باشد.", 422);
    public static Error NumberMustGreaterZiro = new("InvalidArguments", "تعداد وارد شده باید بزرگرتر صفر باشد.", 422);
    public static Error LengthIsEmpty = new("InvalidArguments", "طول خالی است.", 422);
    public static Error WidthIsEmpty = new("InvalidArguments", "عرض خالی است.", 422);
    public static Error HeightIsEmpty = new("InvalidArguments", "ارتفاع خالی است.", 422);
    public static Error WeightIsEmpty = new("InvalidArguments", "وزن خالی است.", 422);
    public static Error NumberIsEmpty = new("InvalidArguments", "تعداد خالی است.", 422);

}
