
namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredThirdParties;

public record GetFilteredThirdPartiesRequest(
 List<long>? Ids,
 string? FilterData,
 bool? IsActive,
 bool? IsIndividual,
 List<string>? SkillIds,
 int PageIndex,
 int PageSize
    );
