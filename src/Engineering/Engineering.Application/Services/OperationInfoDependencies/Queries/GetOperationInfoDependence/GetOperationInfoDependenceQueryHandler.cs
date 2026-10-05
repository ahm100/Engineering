using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfoDependency = Engineering.Domain.Entities.OperationInfos.OperationInfoDependency;

namespace Engineering.Application.Services.OperationInfoDependencies.Queries.GetOperationInfoDependence;

public class GetOperationInfoDependenceQueryHandler : IQueryHandler<GetOperationInfoDependenceQuery, OperationInfoDependency?>
{
    private readonly ILogger<GetOperationInfoDependenceQueryHandler> _logger;
    private readonly IOperationInfoDependencyRepository _repository;

    public GetOperationInfoDependenceQueryHandler(ILogger<GetOperationInfoDependenceQueryHandler> logger, IOperationInfoDependencyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationInfoDependency?>> Handle(GetOperationInfoDependenceQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetOperationInfoDependence(request.OperationInfoId, ct);

            return result ?? Result.Failure<OperationInfoDependency?>(OperationInfoDependencyErrors.DependencyWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationInfoDependency?>(SharedErrors.UnknownError);
        }
    }
}
