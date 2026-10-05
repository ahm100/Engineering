
namespace Engineering.Domain.Errors;

public static class ProjectOperationTemporaryDailyErrors
{
    public static Error TemporaryDailyDocumentWithIdNotFound = new("NotFound", "هیچ پیوست کارکرد موقت ای با این شناسه یافت نشد.", 404);
    public static Error TemporaryDailyWithIdNotFound = new("NotFound", "هیچ کارکرد موقت ای با این شناسه یافت نشد.", 404);
    public static Error FilteredTemporaryDailyNotFound = new("NotFound", "هیچ کارکرد موقت ای با این اطلاعات یافت نشد.", 204);
    public static Error IsDeleted = new("NotFound", "این کارکرد موقت شرح عملیات حذف شده است.", 204);

    public static Error TemporaryDailyIsEmpty = new("InvalidArguments", "کارکرد موقت خالی است.", 422);
    public static Error CreatorIsEmpty = new("InvalidArguments", "درخواست دهنده خالی است.", 422);
    public static Error CostCenterIsEmpty = new("InvalidArguments", "مرکز هزینه خالی است.", 422);
    public static Error CostCenterIdIsEmpty = new("InvalidArguments", "شناسه مرکز هزینه خالی است.", 422);
    public static Error ProjectIsEmpty = new("InvalidArguments", "پروژه خالی است.", 422);
    public static Error ProjectIdIsEmpty = new("InvalidArguments", "شناسه پروژه خالی است.", 422);
    public static Error ProjectOperationIsEmpty = new("InvalidArguments", "شرح عملیات پروژه خالی است.", 422);
    public static Error ProjectOperationIdIsEmpty = new("InvalidArguments", "شناسه شرح عملیات پروژه خالی است.", 422);
    public static Error IdIsEmpty = new("InvalidArguments", "شناسه کارکرد موقت خالی است.", 422);
    public static Error DocumentIdIsEmpty = new("InvalidArguments", "شناسه پیوست کارکرد موقت خالی است.", 422);
    public static Error StartDateIsEmpty = new("InvalidArguments", "تاریخ شروع کارکرد موقت خالی است.", 422);
    public static Error EndDateIsEmpty = new("InvalidArguments", "تاریخ پایان کارکرد موقت خالی است.", 422);
    public static Error UrlIsEmpty = new("InvalidArguments", "پیوست خالی است.", 422);
    public static Error TemporaryDailyStatusIsEmpty = new("InvalidArguments", "وضعیت کارکرد موقت خالی است.", 422);
    public static Error UnValidId = new("InvalidArguments", "اطلاعات وارد شده نامعتبر است.", 422);
    public static Error UnValidStatus = new("InvalidArguments", "وضعیت نامعتبر است.", 422);
    public static Error UnValidDate = new("InvalidArguments", "تاریخ شروع از تاریخ پایان بزرگتر است.", 422);
    public static Error UnValidRequester = new("InvalidArguments", "درخواست دهنده نامعتبر است.", 422);
}
