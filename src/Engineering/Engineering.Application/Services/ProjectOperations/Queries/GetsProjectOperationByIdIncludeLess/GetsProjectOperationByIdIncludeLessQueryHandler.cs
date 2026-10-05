using Engineering.Application.Abstractions.Data.ProjectOperations;
using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetsProjectOperationByIdIncludeLess;

public class GetsProjectOperationByIdIncludeLessQueryHandler : IQueryHandler<GetsProjectOperationByIdIncludeLessQuery, DataResult<List<ProjectOperation>>>
{
    private readonly ILogger<GetsProjectOperationByIdIncludeLessQueryHandler> _logger;
    private readonly IProjectOperationRepository _repository;

    public GetsProjectOperationByIdIncludeLessQueryHandler(ILogger<GetsProjectOperationByIdIncludeLessQueryHandler> logger, IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ProjectOperation>>?>> Handle(GetsProjectOperationByIdIncludeLessQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsProjectOperationByIdIncludeLess(request.Ids, ct);
            return result.Data.Any() ?
               new DataResult<List<ProjectOperation>>
               {
                   Data = result.Data,
                   RowCount = result.RowCount
               } : Result.Failure<DataResult<List<ProjectOperation>>>(ProjectOperationErrors.ProjectOperationWithIdNotFound);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ProjectOperation>>>(SharedErrors.UnknownError);
        }
    }
}
