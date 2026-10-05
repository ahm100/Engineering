using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByIdByProjectOperations;

public class GetOperationInfoByIdByProjectOperationsQueryHandler : IQueryHandler<GetOperationInfoByIdByProjectOperationsQuery, OperationInfo>
{
    private readonly ILogger<GetOperationInfoByIdByProjectOperationsQueryHandler> _logger;
    private readonly IOperationInfoRepository _repository;

    public GetOperationInfoByIdByProjectOperationsQueryHandler(ILogger<GetOperationInfoByIdByProjectOperationsQueryHandler> logger, IOperationInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationInfo?>> Handle(GetOperationInfoByIdByProjectOperationsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetOperationInfoByIdByProjectOperations(request.Id, ct);
            return result ?? Result.Failure<OperationInfo>(OperationInfoErrors.OperationInfoWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationInfo>(SharedErrors.UnknownError);
        }
    }
}
