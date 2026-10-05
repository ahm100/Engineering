using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.Companies.Models.GetFilteredCompaniesByIds;
using CompanyModel = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;


namespace Engineering.Application.WebServices.MetaDataServices.Companies.Queries.GetFilteredCompaniesByIds;

public class GetFilteredCompaniesByIdsQueryHandler : IQueryHandler<GetFilteredCompaniesByIdsQuery, DataResult<List<CompanyModel>>>
{
    private readonly ILogger<GetFilteredCompaniesByIdsQueryHandler> _logger;
    private readonly IMetaDataService _metaDataService;

    public GetFilteredCompaniesByIdsQueryHandler(IMetaDataService metaDataService, ILogger<GetFilteredCompaniesByIdsQueryHandler> logger)
    {
        _logger = logger;
        _metaDataService = metaDataService;
    }

    public async Task<Result<DataResult<List<CompanyModel>>?>> Handle(GetFilteredCompaniesByIdsQuery request, CT ct)
    {
        try
        {
            var companies = await _metaDataService.GetFilteredCompaniesByIds(request.Adapt<GetFilteredCompaniesByIdsRequest>(), ct);

            return (companies?.Value?.Data?.Any()) ?? false ?
               new DataResult<List<CompanyModel>>
               {
                   Data = companies.Value.Data,
                   RowCount = companies.Value.RowCount
               } : Result.Failure<DataResult<List<CompanyModel>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<CompanyModel>>>(SharedErrors.UnknownError);
        }
    }
}