using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfoDependency = Engineering.Domain.Entities.OperationInfos.OperationInfoDependency;

namespace Engineering.Application.Services.OperationInfoDependencies.Queries.GetOperationInfoDependencies;

public class GetOperationInfoDependenciesQueryHandler : IQueryHandler<GetOperationInfoDependenciesQuery, DataResult<List<OperationInfoDependency>>>
{
    private readonly IOperationInfoDependencyRepository _repository;
    private readonly ILogger<GetOperationInfoDependenciesQueryHandler> _logger;

    public GetOperationInfoDependenciesQueryHandler(ILogger<GetOperationInfoDependenciesQueryHandler> logger, IOperationInfoDependencyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<OperationInfoDependency>>?>> Handle(GetOperationInfoDependenciesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetOperationInfoDependencies(request.OperationInfoId, request.DependencyType, request.OrderBy, request.PageIndex, request.PageSize, ct);

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