
namespace Engineering.Domain.Errors;

public class ContractorContractDetailThirdPartyErrors
{
    public static Error InValidThirdPartyId = new("InvalidArguments", "شناسه پرسنل معتبر نیست.", 422);
    public static Error InValidThirdParty = new("InvalidArguments", "پرسنل معتبر نیست.", 422);
    public static Error IsDeleted = new("NotFound", "پرسنل شرح قرارداد پیمانکار حذف شده است.", 204);
}
