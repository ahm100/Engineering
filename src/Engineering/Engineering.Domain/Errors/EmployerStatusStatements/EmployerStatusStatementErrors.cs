
namespace Engineering.Domain.Errors;

public static class EmployerStatusStatementErrors
{
    public static Error UnValidId = new("InvalidArguments", "شناسه صورت وضعیت کارفرما نامعتبر است.", 422);
    public static Error EmployerIdEmpty = new("InvalidArguments", "شناسه کارفرما خالی است.", 422);
    public static Error CostCenterIdIsEmpty = new("InvalidArguments", "شناسه مرکزهزینه خالی است.", 422);
    public static Error ProjectIdEmpty = new("InvalidArguments", "شناسه پروژه خالی است.", 422);
    public static Error EmployerStatusStatementIdEmpty = new("InvalidArguments", "صورت وضعیت کارفرما خالی است.", 422);
    public static Error ProjectOperationIdEmpty = new("InvalidArguments", "شناسه شرح عملیات پروژه خالی است.", 422);
    public static Error EmployerContractIdEmpty = new("InvalidArguments", "شناسه قرارداد کارفرما خالی است.", 422);
    public static Error ContractCodeEmpty = new("InvalidArguments", "کد قرارداد خالی است.", 422);
    public static Error RequesterEmpty = new("InvalidArguments", "طرف حسابی یافت نشد.", 422);
    public static Error StartDateEmpty = new("InvalidArguments", "تاریخ شروع نامعتبر است.", 422);
    public static Error RequesterIdsAreEmpty = new("InvalidArguments", "درخواست کننده ای تا به حال ثبت نشده است.", 204);
    public static Error EndDateEmpty = new("InvalidArguments", "تاریخ پایان نامعتبر است.", 422);
    public static Error LastEmployerStatusStatementInValid = new("NotFound", "شناسه آخرین صورت وضعیت کارفرما نامعتبر است.", 204);
    public static Error DateTimeNotValid = new("InvalidArguments", "تاریخ پایان باید بزرگتر از تاریخ شروع باشد.", 422);
    public static Error DateTimeNotValidToDay = new("InvalidArguments", "تاریخ شروع یا تاریخ پایان نمیتواند بیشتر از امروز باشد.", 422);
    public static Error DateTimeOfLastEmployerStatusStatementNotValid = new("InvalidArguments", "تاریخ شروع این صورت وضعیت نمیتواند کمتر از تاریخ پایان آخرین صورت وضعیت باشد.", 422);
    public static Error PONoHavePrice = new("InvalidArguments", "شرح عملیات شما هیچ مبلغی ندارد.", 422);

    public static Error EmployerStatusStatementIdIsEmpty = new("InvalidArguments", "شناسه صورت وضعیت کارفرما نامعتبر است.", 422);
    public static Error EmployerStatusStatementProjectOperationIdIsEmpty = new("InvalidArguments", "شناسه شرح عملیات صورت وضعیت کارفرما نامعتبر است.", 422);
    public static Error StatusStatementCodeEmpty = new("InvalidArguments", "کد صورت وضعیت کارفرما نامعتبر است.", 422);
    public static Error ProjectOperationIdsNotValid = new("InvalidArguments", "شناسه ای از شرح عملیات پروژه صحیح نمیباشد.", 422);
    public static Error UnvalidContractNotValid = new("InvalidArguments", "شرح عملیات های با قرارداد شما باید حتما از یک قرارداد باشند.", 422);
    public static Error AllProjectOperationNoHaveContract = new("InvalidArguments", "تمامی شرح عملیات های پروژه انتخابی بدون قرارداد هستند، بای یک شرح علملیات پروژه با قرارداد انتخاب کنید.", 422);
    public static Error ProjectOperationNotFound = new("InvalidArguments", "هیج شرح عملیات پروژه ای با این اطلاعات یافت نشد.", 422);

    public static Error DataNotFound = new("NotFound", "هیچ صورت وضعیت کارفرما ای با این فیلتر یافت نشد.", 204);

    public static Error CanNottDelete = new("InvalidArguments", "این برآورد به دلیل داشتن وابستگی اطلاعاتی قابل حذف نمیباشد.", 422);
    public static Error ChangeToPending = new("InvalidArguments", "شما باید از وضعیت ثبت الویه به وضعیت در حال بررسی، تغییر وضعیت بدین، در غیر اینصورت امکان پذیر نمیباشد.", 422);
    public static Error ChangeToConfirmRequest = new("InvalidArguments", "شما باید از وضعیت درحال بررسی به وضعیت تایید درخواست، تغییر وضعیت بدین، در غیر اینصورت امکان پذیر نمیباشد.", 422);
    public static Error ChangeToRequestRejection = new("InvalidArguments", "شما باید از وضعیت درحال بررسی به وضعیت رد درخواست، تغییر وضعیت بدین، در غیر اینصورت امکان پذیر نمیباشد.", 422);

    public static Error ChangeToSendToModerator = new("InvalidArguments", "شما در وضعیت مناسب نمیباشید.", 422);
}
