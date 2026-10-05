using Engineering.Application.WebServices.MetaDataServices.Contractors.Models;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetFilteredThirdParties;

public record GetFilteredThirdPartiesQuery(
 List<long>? Ids,
 string? FilterData,
 bool? IsActive,
 bool? IsIndividual,
 List<string>? SkillIds,
 int PageIndex,
 int PageSize
    ) : IQuery<DataResult<List<Contractor>>>;
