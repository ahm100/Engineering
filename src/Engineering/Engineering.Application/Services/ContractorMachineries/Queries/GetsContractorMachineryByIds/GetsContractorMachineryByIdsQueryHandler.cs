using Engineering.Application.Abstractions.Data.ContractorMachineries;
using ContractorMachinery = Engineering.Domain.Entities.ContractorMachineries.ContractorMachinery;

namespace Engineering.Application.Services.ContractorMachineries.Queries.GetsContractorMachineryByIds;

public class GetsContractorMachineryByIdsQueryHandler : IQueryHandler<GetsContractorMachineryByIdsQuery, DataResult<List<ContractorMachinery?>>>
{
    private readonly IContractorMachineryRepository _repository;
    private readonly ILogger<GetsContractorMachineryByIdsQueryHandler> _logger;

    public GetsContractorMachineryByIdsQueryHandler(ILogger<GetsContractorMachineryByIdsQueryHandler> logger, IContractorMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ContractorMachinery?>>?>> Handle(GetsContractorMachineryByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsContractorMachineryByIds(request.ids, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<ContractorMachinery?>>
                {
                    Data = result.Data!,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ContractorMachinery?>>>(ContractorMachineryErrors.FilteredContractorMachineryNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ContractorMachinery?>>>(SharedErrors.UnknownError);
        }
    }
}