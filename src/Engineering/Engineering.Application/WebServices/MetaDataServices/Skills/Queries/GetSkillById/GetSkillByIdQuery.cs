
namespace Engineering.Application.WebServices.MetaDataServices.Skills.Queries.GetSkillById;

public record GetSkillByIdQuery(
    long Id
    ) : IQuery<Skill?>;
