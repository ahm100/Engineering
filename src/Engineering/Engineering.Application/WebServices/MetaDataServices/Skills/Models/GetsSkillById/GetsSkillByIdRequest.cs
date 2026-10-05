
namespace Engineering.Application.WebServices.MetaDataServices.Skills.Models.GetsSkillById;

public record GetsSkillByIdRequest(
    List<long> Ids,
    bool IgnoreQuery,
    int PageIndex,
    int PageSize
    );
