using CompanyModel = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;

namespace Engineering.Application.WebServices.MetaDataServices.Companies.Queries.GetFilteredCompaniesByIds;

public record GetFilteredCompaniesByIdsQuery(
    List<long> Ids,
    string? FilterData,
    int? PageIndex,
    int? PageSize) : IQuery<DataResult<List<CompanyModel>>>;