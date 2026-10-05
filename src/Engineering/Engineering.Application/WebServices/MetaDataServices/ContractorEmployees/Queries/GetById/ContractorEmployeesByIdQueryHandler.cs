using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.ContractorEmployees.Models;
using Engineering.Application.WebServices.MetaDataServices.ContractorEmployees.Models.GetById;

namespace Engineering.Application.WebServices.MetaDataServices.ContractorEmployees.Queries.GetById;

public class ContractorEmployeesByIdQueryHandler : IQueryHandler<ContractorEmployeesByIdQuery, DataResult<List<ContractorEmployee>>>
{
    private readonly IMetaDataService _metaDataService;
    private readonly ILogger<ContractorEmployeesByIdQueryHandler> _logger;

    public ContractorEmployeesByIdQueryHandler(IMetaDataService metaDataService,
                                               ILogger<ContractorEmployeesByIdQueryHandler> logger)
    {
        _metaDataService = metaDataService;
        _logger = logger;
    }

    public async Task<Result<DataResult<List<ContractorEmployee>>?>> Handle(ContractorEmployeesByIdQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetContractorEmployees(request.Adapt<ContractorEmployeesByIdRequest>(), ct);

            return (result?.Value?.Data?.Any()) ?? false ?
                new DataResult<List<ContractorEmployee>>
                {
                    Data = result.Value.Data,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<ContractorEmployee>>>(SharedErrors.ItemNotFound);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ContractorEmployee>>>(SharedErrors.UnknownError);
        }
    }
}
