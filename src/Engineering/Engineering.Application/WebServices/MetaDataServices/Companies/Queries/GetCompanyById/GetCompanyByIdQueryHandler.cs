using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.Companies.Models.GetCompanyById;
using CompanyModel = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;

namespace Engineering.Application.WebServices.MetaDataServices.Companies.Queries.GetCompanyById;

public class GetCompanyByIdQueryHandler : IQueryHandler<GetCompanyByIdQuery, CompanyModel>
{
    private readonly ILogger<GetCompanyByIdQueryHandler> _logger;
    private readonly IMetaDataService _metaDataService;

    public GetCompanyByIdQueryHandler(ILogger<GetCompanyByIdQueryHandler> logger, IMetaDataService metaDataService)
    {
        _logger = logger;
        _metaDataService = metaDataService;
    }

    public async Task<Result<CompanyModel?>> Handle(GetCompanyByIdQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetCompanyById(request.Adapt<GetCompanyByIdRequest>(), ct);

            return result?.Value ?? Result.Failure<CompanyModel>(SharedErrors.ProviderError);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<CompanyModel>(SharedErrors.UnknownError);
        }
    }
}