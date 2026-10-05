
namespace Engineering.Application.WebServices.MetaDataServices.Skills.Queries.GetFilteredSkillByIds;

public record GetFilteredSkillByIdsQuery(
    List<long> Ids,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<Skill>>>;
