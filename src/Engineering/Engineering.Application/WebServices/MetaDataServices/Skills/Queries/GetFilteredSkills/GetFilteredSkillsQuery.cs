
namespace Engineering.Application.WebServices.MetaDataServices.Skills.Queries.GetFilteredSkills;

public record GetFilteredSkillsQuery(
    string? ThirdPartyId,
    string? Name,
    string? Code,
    string? FilterData,
    bool? IsActive,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<Skill>>>;
