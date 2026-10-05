
namespace Engineering.Domain.Errors;

public static class RequestMachineryHistoryErrors
{
    public static Error InValidRequestMachineryCreator = new("InvalidArguments", "ایجاد کننده نامعتبر است.", 422);
}
