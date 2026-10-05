using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.Contractors.Models.GetContractorById;
using ContractorModel = Engineering.Application.WebServices.MetaDataServices.Contractors.Models.Contractor;

namespace Engineering.Application.WebServices.MetaDataServices.Contractors.Queries.GetContractorById;

public class GetContractorByIdQueryHandler : IQueryHandler<GetContractorByIdQuery, ContractorModel?>
{
    private readonly ILogger<GetContractorByIdQueryHandler> _logger;
    private readonly IMetaDataService _metaDataService;

    public GetContractorByIdQueryHandler(ILogger<GetContractorByIdQueryHandler> logger, IMetaDataService metaDataService)
    {
        _logger = logger;
        _metaDataService = metaDataService;
    }

    public async Task<Result<ContractorModel?>> Handle(GetContractorByIdQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetContractorById(request.Adapt<GetContractorByIdRequest>(), ct);

            var data = result?.Value?.Data?.FirstOrDefault();

            return data ?? Result.Failure<ContractorModel?>(SharedErrors.ProviderError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorModel?>(SharedErrors.UnknownError);
        }
    }
}