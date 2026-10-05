
namespace Engineering.Domain.Errors;

public static class RequestContractorHistoryErrors
{
    public static Error InValidRequestContractorCreator = new("InvalidArguments", "ایجاد کننده نامعتبر است.", 422);
}
