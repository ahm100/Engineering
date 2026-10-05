
namespace Engineering.Application.WebServices.MetaDataServices.Skills.Queries.GetsSkillById;

public record GetsSkillByIdQuery(
    List<long> Ids,
    bool IgnoreQuery,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<Skill>>>;
