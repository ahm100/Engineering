using Engineering.Application.WebServices.MetaDataServices.ThirdPartySkills.Models.GetThirdPartiesSkills;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdPartySkills.Queries.GetThirdPartiesSkills;

public record GetThirdPartiesSkillsQuery(
    List<long> ThirdPartyIds
    ) : IQuery<DataResult<List<GetThirdPartiesSkillsModel?>?>?>;