
namespace Engineering.Application.WebServices.MetaDataServices.Skills.Queries.GetSkillByCode;

public record GetSkillByCodeQuery(
    string Code
    ) : IQuery<Skill?>;
