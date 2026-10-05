using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Application.Services.ProjectOperations.Models.Models;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetsFilteredByProject;

public class GetsFilteredByProjectQueryHandler : IQueryHandler<GetsFilteredByProjectQuery, DataResult<List<GetsProjectOperationByProjectModel>>>
{
    private readonly IProjectOperationRepository _repository;
    private readonly ILogger<GetsFilteredByProjectQueryHandler> _logger;

    public GetsFilteredByProjectQueryHandler(ILogger<GetsFilteredByProjectQueryHandler> logger, IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsProjectOperationByProjectModel>>?>> Handle(GetsFilteredByProjectQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsFilteredByProject(request.ProjectId, request.CategoryId, request.BranchId, request.SeasonId,
                request.ContractorIds, request.StartDate, request.EndDate, request.FilterData, request.OrderBy, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<GetsProjectOperationByProjectModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsProjectOperationByProjectModel>>>(ProjectOperationErrors.DataNotFoundWithFilters);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsProjectOperationByProjectModel>>>(SharedErrors.UnknownError);
        }
    }
}