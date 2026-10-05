
namespace Engineering.Application.WebServices.MetaDataServices.Skills.Models.GetFilteredSkillByIds;

public record GetFilteredSkillByIdsRequest(
    List<long> Ids,
    string? FilterData,
    int PageIndex,
    int PageSize
    );
