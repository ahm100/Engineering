using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetFilteredProjectOperations;

public class GetFilteredProjectOperationsQueryHandler : IQueryHandler<GetFilteredProjectOperationsQuery, DataResult<List<ProjectOperation>>>
{
    private readonly ILogger<GetFilteredProjectOperationsQueryHandler> _logger;
    private readonly IProjectOperationRepository _repository;

    public GetFilteredProjectOperationsQueryHandler(ILogger<GetFilteredProjectOperationsQueryHandler> logger,
                                                    IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ProjectOperation>>?>> Handle(GetFilteredProjectOperationsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetFilteredProjectOperations(request.ProjectOperationIds, ct);

            return result.Data.Any() ?
                new DataResult<List<ProjectOperation>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ProjectOperation>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ProjectOperation>>>(SharedErrors.UnknownError);
        }
    }
}
