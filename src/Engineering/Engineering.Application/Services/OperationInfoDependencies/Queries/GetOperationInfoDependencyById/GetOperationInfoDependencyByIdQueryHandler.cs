using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfoDependency = Engineering.Domain.Entities.OperationInfos.OperationInfoDependency;

namespace Engineering.Application.Services.OperationInfoDependencies.Queries.GetOperationInfoDependencyById;

public class GetOperationInfoDependencyByIdQueryHandler : IQueryHandler<GetOperationInfoDependencyByIdQuery, OperationInfoDependency?>
{
    private readonly ILogger<GetOperationInfoDependencyByIdQueryHandler> _logger;
    private readonly IOperationInfoDependencyRepository _repository;

    public GetOperationInfoDependencyByIdQueryHandler(ILogger<GetOperationInfoDependencyByIdQueryHandler> logger, IOperationInfoDependencyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationInfoDependency?>> Handle(GetOperationInfoDependencyByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.FindById(request.Id, ct);

            return result ?? Result.Failure<OperationInfoDependency?>(OperationInfoDependencyErrors.DependencyWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationInfoDependency?>(SharedErrors.UnknownError);
        }
    }
}