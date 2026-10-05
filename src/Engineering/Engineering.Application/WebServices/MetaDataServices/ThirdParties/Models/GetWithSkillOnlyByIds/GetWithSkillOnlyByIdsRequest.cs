
namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetWithSkillOnlyByIds;

public record GetWithSkillOnlyByIdsRequest(
    int PageIndex,
    int PageSize,
    List<long> Ids,
    string? FilterData,
    bool IgnoreQuery,
    bool? IsActive
    );
