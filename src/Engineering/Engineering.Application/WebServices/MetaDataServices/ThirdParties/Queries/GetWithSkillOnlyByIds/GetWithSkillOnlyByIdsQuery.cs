using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetWithSkillOnlyByIds;

public record GetWithSkillOnlyByIdsQuery(
    int PageIndex,
    int PageSize,
    List<long> Ids,
    string? FilterData,
    bool IgnoreQuery,
    bool? IsActive
    ) : IQuery<DataResult<List<UserModel?>?>?>;
