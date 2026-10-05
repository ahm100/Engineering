using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsFilteredContractorContractReports;

public class GetsFilteredContractorContractReportsQueryHandler : IQueryHandler<GetsFilteredContractorContractReportsQuery, DataResult<List<ContractorContract>>>
{
    private readonly ILogger<GetsFilteredContractorContractReportsQueryHandler> _logger;
    private readonly IContractorContractRepository _repository;

    public GetsFilteredContractorContractReportsQueryHandler(ILogger<GetsFilteredContractorContractReportsQueryHandler> logger, IContractorContractRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ContractorContract>>?>> Handle(GetsFilteredContractorContractReportsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsFilteredContractorContractReports(
                request.Ids,
                request.ContractorId,
                request.CostCenterId,
                request.ProjectIds,
                request.ContractorContractIds,
                request.FromDate,
                request.ToDate,
                request.CompanyId,
                request.FilterData,
                request.OrderBy,
                request.PageIndex,
                request.PageSize, ct);

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
