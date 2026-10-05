namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetWithSelectedSkillId;

public record GetWithSelectedSkillIdRequest(
    List<long> ThirdPartyIds,
    List<long> SkillIds,
    string? FilterData
    );
