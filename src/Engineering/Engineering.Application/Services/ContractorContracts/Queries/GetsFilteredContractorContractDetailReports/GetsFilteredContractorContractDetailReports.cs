using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsFilteredContractorContractDetailReports;

public class GetsFilteredContractorContractDetailReportsQueryHandler : IQueryHandler<GetsFilteredContractorContractDetailReportsQuery, DataResult<List<ContractorContractDetail>>>
{
    private readonly ILogger<GetsFilteredContractorContractDetailReportsQueryHandler> _logger;
    private readonly IContractorContractDetailRepository _repository;

    public GetsFilteredContractorContractDetailReportsQueryHandler(ILogger<GetsFilteredContractorContractDetailReportsQueryHandler> logger, IContractorContractDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ContractorContractDetail>>?>> Handle(GetsFilteredContractorContractDetailReportsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsFilteredContractorContractDetailReports(
                request.Ids,
                request.ContractorContractId,
                request.ContractorId,
                request.FromDate,
                request.ToDate,
                request.CompanyId,
                request.FilterData,
                request.OrderBy,
                request.PageIndex,
                request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<ContractorContractDetail>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ContractorContractDetail>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ContractorContractDetail>>>(SharedErrors.UnknownError);
        }
    }
}
