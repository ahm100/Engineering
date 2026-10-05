using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetFilteredContractorContractDetails;

public class GetFilteredContractorContractDetailsQueryHandler : IQueryHandler<GetFilteredContractorContractDetailsQuery, DataResult<List<ContractorContractDetail>>>
{
    private readonly ILogger<GetFilteredContractorContractDetailsQueryHandler> _logger;
    private readonly IContractorContractDetailRepository _repository;

    public GetFilteredContractorContractDetailsQueryHandler(
        ILogger<GetFilteredContractorContractDetailsQueryHandler> logger,
        IContractorContractDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ContractorContractDetail>>?>> Handle(GetFilteredContractorContractDetailsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetFilteredContractorContractDetail(
                request.ProjectOperationServiceId,
                request.ProjectOperationId,
                request.StratDate,
                request.EndDate,
                request.ContractorId,
                request.FilterData,
                request.CompanyId,
                request.OrderBy,
                request.PageIndex,
                request.PageSize,
                ct);

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
