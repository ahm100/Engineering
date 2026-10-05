using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetFilteredContractorContractsByContractor;

public class GetFilteredContractorContractsByContractorQueryHandler : IQueryHandler<GetFilteredContractorContractsByContractorQuery, DataResult<List<ContractorContract>>>
{
    private readonly ILogger<GetFilteredContractorContractsByContractorQueryHandler> _logger;
    private readonly IContractorContractRepository _repository;

    public GetFilteredContractorContractsByContractorQueryHandler(ILogger<GetFilteredContractorContractsByContractorQueryHandler> logger,
                                                      IContractorContractRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ContractorContract>>?>> Handle(GetFilteredContractorContractsByContractorQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetFilteredByContractorIdAsync(request.ContrctorId, request.CostCenterId, request.Projects,
                request.Contracts, request.FromDate, request.ToDate, request.FilterData, request.CompanyId, request.OrderBy, request.PageIndex, request.PageSize, ct);

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
