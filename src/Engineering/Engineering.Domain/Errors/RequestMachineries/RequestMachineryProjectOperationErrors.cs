
namespace Engineering.Domain.Errors;

public static class RequestMachineryProjectOperationErrors
{
    public static Error RequestMachineryProjectOperationNotFound = new("NotFound", "شرح عملیات درخواست ماشین آلات یافت نشد.", 404);

    public static Error InValidProjectOperation = new("InvalidArguments", "شرح عملیات پروژه نامعتبر است.", 422);
    public static Error InValidRequestMachinery = new("InvalidArguments", "درخواست ماشین آلات نامعتبر است.", 422);
    public static Error IsDeleted = new("NotFound", "درخواست ماشین آلات شرح عملیاتحذف شده است.", 204);
}
