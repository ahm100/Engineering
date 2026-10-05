
namespace Engineering.Domain.Errors;

public static class MachineErrors
{
    public static Error NameIsDuplicate = new("Duplicate", "نام ماشین تکراری است.", 409);
    public static Error CodeIsDuplicate = new("Duplicate", "کد ماشین تکراری است.", 409);

    public static Error IsActive = new("InvalidArguments", "وضعیت ماشین فعال میباشد.", 422);
    public static Error IsInactive = new("InvalidArguments", "وضعیت ماشین غیرفعال میباشد.", 422);
    public static Error CanNottDelete = new("InvalidArguments", "این ماشین به دلیل داشتن وابستگی اطلاعاتی قابل حذف نمیباشد.", 422);
    public static Error CanNottDeleteBecauseOfTransportationRequest = new("InvalidArguments", " به دلیل داشتن درخواست ترابری این ماشین قابل حذف نمیباشد.", 422);

    public static Error IdIsEmpty = new("InvalidArguments", "شناسه ماشین خالی است.", 422);
    public static Error NameIsEmpty = new("InvalidArguments", "نام ماشین خالی است.", 422);
    public static Error CodeIsEmpty = new("InvalidArguments", "کد ماشین خالی است.", 422);
    public static Error FromWeightIsEmpty = new("InvalidArguments", "ماشین از وزن خالی است.", 422);
    public static Error UntilWeightIsEmpty = new("InvalidArguments", "ماشین تا وزن خالی است.", 422);
    public static Error IsActiveIsEmpty = new("InvalidArguments", "وضعیت ماشین خالی است.", 422);
    public static Error MachineWithIdNotFound = new("NotFound", "هیچ ماشین ای با این شناسه یافت نشد.", 404);
    public static Error MachineWithCodeNotFound = new("NotFound", "هیچ ماشین ای با این کد یافت نشد.", 404);
    public static Error MachineWithNameNotFound = new("NotFound", "هیچ ماشین ای با این نام یافت نشد.", 404);
    public static Error FilteredMachineNotFound = new("NotFound", "هیچ ماشین ای با این اطلاعات یافت نشد.", 204);
    public static Error MachineChildNotFound = new("NotFound", "ماشین انتخابی هیچ زیرشاخه ای ندارد.", 204);
    public static Error IsDeleted = new("NotFound", "این ماشین حذف شده است.", 204);

    public static Error UnValidId = new("InvalidArguments", "شناسه ماشین نامعتبر است.", 422);
    public static Error UnValidData = new("InvalidArguments", "اطلاعات وارد شده نامعتبر است.", 422);

    public static Error HaveChild = new("InvalidArguments", "ماشین انتخابی به دلیل وابستگی اطلاعاتی، قابل تغییر نمیباشد.", 422);

}