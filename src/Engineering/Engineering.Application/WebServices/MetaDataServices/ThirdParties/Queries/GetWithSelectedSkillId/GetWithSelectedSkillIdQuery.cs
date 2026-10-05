using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetWithSelectedSkillId;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetWithSelectedSkillId;

public record GetWithSelectedSkillIdQuery(
    List<long> ThirdPartyIds,
    List<long> SkillIds,
    string? FilterData
    ) : IQuery<DataResult<List<GetWithSelectedSkillIdUserModel?>?>?>;
