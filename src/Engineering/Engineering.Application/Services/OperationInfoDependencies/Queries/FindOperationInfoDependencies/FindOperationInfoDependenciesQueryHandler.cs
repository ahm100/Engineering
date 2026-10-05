using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfoDependency = Engineering.Domain.Entities.OperationInfos.OperationInfoDependency;

namespace Engineering.Application.Services.OperationInfoDependencies.Queries.FindOperationInfoDependencies;

public class FindOperationInfoDependenciesQueryHandler : IQueryHandler<FindOperationInfoDependenciesQuery, DataResult<List<OperationInfoDependency>>>
{
    private readonly ILogger<FindOperationInfoDependenciesQueryHandler> _logger;
    private readonly IOperationInfoDependencyRepository _repository;

    public FindOperationInfoDependenciesQueryHandler(ILogger<FindOperationInfoDependenciesQueryHandler> logger, IOperationInfoDependencyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<OperationInfoDependency>>?>> Handle(FindOperationInfoDependenciesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.FindOperationInfoDependencies(request.OperationInfoId, ct);

            return result.Data.Any() ?
                new DataResult<List<OperationInfoDependency>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<OperationInfoDependency>>>(OperationInfoDependencyErrors.DependencyWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<OperationInfoDependency>>>(SharedErrors.UnknownError);
        }
    }
}