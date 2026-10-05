
namespace Engineering.Domain.Errors;

public static class RequestMachineryProjectOperationDetailErrors
{
    public static Error RequestMachineryProjectOperationDetailNotFound = new("NotFound", "ریز متره درخواست ماشین آلات یافت نشد.", 404);

    public static Error InValidProjectOperationDetail = new("InvalidArguments", "ریز متره نامعتبر است.", 422);
    public static Error InValidRequestMachinery = new("InvalidArguments", "درخواست ماشین آلات نامعتبر است.", 422);
    public static Error InValidRequestMachineryProjectOperationDetail = new("InvalidArguments", "ریز متره درخواست ماشین آلات نامعتبر است.", 422);
    public static Error IsDeleted = new("NotFound", "درخواست ماشین آلات ریزمتره حذف شده است.", 204);
}
