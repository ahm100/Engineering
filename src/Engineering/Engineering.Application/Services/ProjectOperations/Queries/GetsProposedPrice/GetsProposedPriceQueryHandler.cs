using Engineering.Application.Abstractions.Data.ProjectOperations;
using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetsProposedPrice;

public class GetsProposedPriceQueryHandler : IQueryHandler<GetsProposedPriceQuery, DataResult<List<ProjectOperation>>>
{
    private readonly IProjectOperationRepository _repository;
    private readonly ILogger<GetsProposedPriceQueryHandler> _logger;

    public GetsProposedPriceQueryHandler(ILogger<GetsProposedPriceQueryHandler> logger, IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ProjectOperation>>?>> Handle(GetsProposedPriceQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsProposedPrice(request.OperationInfoId, request.FilterData, request.StartDate, request.EndDate, request.EmployerId, request.CostCenterId, request.ProjectId, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<ProjectOperation>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ProjectOperation>>>(ProjectOperationErrors.DataNotFoundWithFilters);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ProjectOperation>>>(SharedErrors.UnknownError);
        }
    }
}