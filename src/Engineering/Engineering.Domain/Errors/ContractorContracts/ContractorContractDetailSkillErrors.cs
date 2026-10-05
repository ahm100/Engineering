
namespace Engineering.Domain.Errors;

public class ContractorContractDetailSkillErrors
{
    public static Error ContractorContractDetailSkillWithIdNotFound = new("NotFound", "شناسه معتبر نیست.", 404);

    public static Error InvalidContractorContractDetailSkillStatus = new("InvalidArguments", "نوع معتبر نیست.", 422);

    public static Error InValidType = new("InvalidArguments", "نوع قرارداد معتبر نیست.", 422);
    public static Error InValidSkillId = new("InvalidArguments", "شناسه تخصص معتبر نیست.", 422);
    public static Error InValidSkill = new("InvalidArguments", "تخصص معتبر نیست.", 422);
    public static Error IsDeleted = new("NotFound", "تخصص شرح قرارداد پیمانکار خالی است.", 204);
}
