using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfoDependency = Engineering.Domain.Entities.OperationInfos.OperationInfoDependency;

namespace Engineering.Application.Services.OperationInfoDependencies.Queries.GetsDependentOnOperationInfo;

public class GetsDependentOnOperationInfoQueryHandler : IQueryHandler<GetsDependentOnOperationInfoQuery, DataResult<List<OperationInfoDependency>>>
{
    private readonly ILogger<GetsDependentOnOperationInfoQueryHandler> _logger;
    private readonly IOperationInfoDependencyRepository _repository;

    public GetsDependentOnOperationInfoQueryHandler(ILogger<GetsDependentOnOperationInfoQueryHandler> logger, IOperationInfoDependencyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<OperationInfoDependency>>?>> Handle(GetsDependentOnOperationInfoQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsDependentOnOperationInfo(request.OperationInfoId, ct);

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