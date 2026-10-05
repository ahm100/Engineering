namespace Engineering.Domain.Errors;

public static class CostOverErrors
{
    public static Error ApprovalConflict = new("WorkflowConflict", "وضعیت هزینه بالاسری با عملیات تأیید سازگار نیست.", 409);
    public static Error ApprovalRecoveryInvalid = new("ApprovalRecoveryInvalid", "شناسه هزینه، شرکت، کاربر و تلاش قبلی برای بازیابی الزامی است.", 400);
    public static Error ApprovalRecoveryCreatorRequired = new("ApprovalRecoveryCreatorRequired", "فقط ایجادکننده هزینه بالاسری مجاز به بازیابی فرایند آن است.", 403);
    public static Error ApprovalRecoveryConflict = new("ApprovalRecoveryConflict", "فقط تلاش جاری با نتیجه نهایی شکست یا لغو قابل بازیابی است.", 409);
    public static Error ApprovalCreatorRequired = new("ApprovalCreatorRequired", "فقط ایجادکننده هزینه بالاسری مجاز به ارسال برای تأیید است.", 403);
    public static Error NameIsDuplicate = new("Duplicate", "نام سربار هزینه تکراری است.", 409);
    public static Error CodeIsDuplicate = new("Duplicate", "کد سربار هزینه تکراری است.", 409);

    public static Error IsActive = new("InvalidArguments", "این سربار هزینه فعال است.", 422);
    public static Error IsInactive = new("InvalidArguments", "این سربار هزینه غیرفعال است.", 422);
    public static Error CanNottDelete = new("InvalidArguments", "این سربار هزینه به دلیل داشتن وابستگی اطلاعاتی قابل حذف نمیباشد.", 422);
    public static Error CanNottDeleteBecauseOfContract = new("InvalidArguments", "به دلیل داشتن قرارداد این سربار هزینه قابل حذف نمیباشد.", 422);
    public static Error CanNottDeleteBecauseOfImpacts = new("InvalidArguments", "به دلیل داشتن تاثیر در قرارداد این سربار هزینه قابل حذف نمیباشد.", 422);

    public static Error CostOverWithIdNotFound = new("NotFound", "هیچ سربار هزینه ای با این شناسه یافت نشد.", 404);
    public static Error CostOverWithCodeNotFound = new("NotFound", "هیچ سربار هزینه ای با این کد یافت نشد.", 404);
    public static Error CostOverWithNameNotFound = new("NotFound", "هیچ سربار هزینه ای با این نام یافت نشد.", 404);
    public static Error FilteredCostOverNotFound = new("NotFound", "هیچ سربار هزینه ای با این اطلاعات یافت نشد.", 204);
    public static Error CostOverChildNotFound = new("NotFound", "هیچ هزینه سرباری  با این اطلاعات یافت نشد.", 204);
    public static Error IsDeleted = new("NotFound", "این سربار هزینه حذف شده است.", 204);

    public static Error UnValidId = new("InvalidArguments", "سربار هزینه با این شناسه نامعتبر است.", 422);
    public static Error IdIsEmpty = new("InvalidArguments", "شناسه خالی است!", 422);
    public static Error CostOverNameIsEmpty = new("InvalidArguments", "نام هزینه سربار خالی است!", 422);
    public static Error CostOverCodeIsEmpty = new("InvalidArguments", "کد هزینه سربار خالی است!", 422);
    public static Error IsActiveIsEmpty = new("InvalidArguments", "وضعیت رشته خالی است!", 422);
    public static Error FilterDataIsEmpty = new("InvalidArguments", "دیتای فیلتر خالی است!", 422);

    public static Error HaveChild = new("InvalidArguments", "سربار هزینه دارای وابستگی اطلاعاتی است و قابل تغییر نیست.", 422);
}
