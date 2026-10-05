using Engineering.Application.WebServices.MetaDataServices.ThirdPartySkills.Models.AddSkillForThirdParty;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdPartySkills.Commands.AddSkillForThirdPartyCommand;

public record AddSkillForThirdPartyCommand(
    long ThirdPartyId,
    long SkillId
    ) : IQuery<AddSkillForThirdPartyModel?>;
