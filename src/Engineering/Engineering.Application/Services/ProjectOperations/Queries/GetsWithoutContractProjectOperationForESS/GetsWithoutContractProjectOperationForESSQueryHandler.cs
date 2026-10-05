using Engineering.Application.Abstractions.Data.ProjectOperations;
using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetsWithoutContractProjectOperationForESS;

public class GetsWithoutContractProjectOperationForESSQueryHandler : IQueryHandler<GetsWithoutContractProjectOperationForESSQuery, DataResult<List<ProjectOperation>>>
{
    private readonly IProjectOperationRepository _repository;
    private readonly ILogger<GetsWithoutContractProjectOperationForESSQueryHandler> _logger;

    public GetsWithoutContractProjectOperationForESSQueryHandler(ILogger<GetsWithoutContractProjectOperationForESSQueryHandler> logger, IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ProjectOperation>>?>> Handle(GetsWithoutContractProjectOperationForESSQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsWithoutContractProjectOperationForESS(request.ProjectId, request.FilterData, request.PageIndex, request.PageSize, ct);

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
