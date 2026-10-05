
namespace Engineering.Application.WebServices.MetaDataServices.Skills.Models.GetFilteredSkills;

public record GetFilteredSkillsRequest(
    string? ThirdPartyId,
    string? Name,
    string? Code,
    string? FilterData,
    bool? IsActive,
    int PageIndex,
    int PageSize
    );
