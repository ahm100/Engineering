using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsContractorContractByContractorId;

public class GetsContractorContractByContractorIdQueryHandler : IQueryHandler<GetsContractorContractByContractorIdQuery, DataResult<List<ContractorContract>>>
{
    private readonly ILogger<GetsContractorContractByContractorIdQueryHandler> _logger;
    private readonly IContractorContractRepository _repository;

    public GetsContractorContractByContractorIdQueryHandler(ILogger<GetsContractorContractByContractorIdQueryHandler> logger,
                                                      IContractorContractRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ContractorContract>>?>> Handle(GetsContractorContractByContractorIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsContractorContractByContractorId(request.ContractorId, request.CostCenterId, request.Projects, request.FilterData, request.OrderBy, request.PageIndex, request.PageSize, request.CompanyId, ct);

            return result.Data.Any() ?
                new DataResult<List<ContractorContract>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ContractorContract>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ContractorContract>>>(SharedErrors.UnknownError);
        }
    }
}
