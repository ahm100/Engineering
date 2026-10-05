namespace Engineering.Application.WebServices.MetaDataServices.Companies.Models.GetFilteredCompaniesByIds;

public record GetFilteredCompaniesByIdsRequest(
    List<long> Ids,
    string? FilterData,
    int? PageIndex,
    int? PageSize);