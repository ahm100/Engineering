using Engineering.Application.Abstractions.Data.ContractorMachineries;
using ContractorMachinery = Engineering.Domain.Entities.ContractorMachineries.ContractorMachinery;

namespace Engineering.Application.Services.ContractorMachineries.Queries.GetActiveContractorMachineries;

public class GetActiveContractorMachineriesQueryHandler : IQueryHandler<GetActiveContractorMachineriesQuery, DataResult<List<ContractorMachinery>>>
{
    private readonly IContractorMachineryRepository _repository;
    private readonly ILogger<GetActiveContractorMachineriesQuery> _logger;

    public GetActiveContractorMachineriesQueryHandler(ILogger<GetActiveContractorMachineriesQuery> logger, IContractorMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ContractorMachinery>>?>> Handle(GetActiveContractorMachineriesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetActiveContractorMachineries(request.MachineryIds, request.ContractorIds, request.Unit, request.FromDate, request.ToDate,
                request.FilterData, request.CompanyId, request.OrderBy, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<ContractorMachinery>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ContractorMachinery>>>(ContractorMachineryErrors.FilteredContractorMachineryNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ContractorMachinery>>>(SharedErrors.UnknownError);
        }
    }
}