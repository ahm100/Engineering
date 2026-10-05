using Engineering.Application.Abstractions.Data.ContractorMachineries;
using ContractorMachinery = Engineering.Domain.Entities.ContractorMachineries.ContractorMachinery;

namespace Engineering.Application.Services.ContractorMachineries.Queries.GetsByContractorId;

public class GetsByContractorIdQueryHandler : IQueryHandler<GetsByContractorIdQuery, DataResult<List<ContractorMachinery>>>
{
    private readonly IContractorMachineryRepository _repository;
    private readonly ILogger<GetsByContractorIdQueryHandler> _logger;

    public GetsByContractorIdQueryHandler(ILogger<GetsByContractorIdQueryHandler> logger, IContractorMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ContractorMachinery>>?>> Handle(GetsByContractorIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsByContractorId(request.ContractorId, request.FilterData, request.IsActive, request.CompanyId, request.OrderBy,
                request.PageIndex, request.PageSize, ct);

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