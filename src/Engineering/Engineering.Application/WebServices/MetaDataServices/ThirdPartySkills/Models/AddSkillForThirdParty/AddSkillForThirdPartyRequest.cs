namespace Engineering.Application.WebServices.MetaDataServices.ThirdPartySkills.Models.AddSkillForThirdParty;

public record AddSkillForThirdPartyRequest(
    long ThirdPartyId,
    long SkillId
    );
