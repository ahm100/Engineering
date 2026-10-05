using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByIdByGroupRelations;

public class GetOperationInfoByIdByGroupRelationsQueryHandler : IQueryHandler<GetOperationInfoByIdByGroupRelationsQuery, OperationInfo>
{
    private readonly ILogger<GetOperationInfoByIdByGroupRelationsQueryHandler> _logger;
    private readonly IOperationInfoRepository _repository;

    public GetOperationInfoByIdByGroupRelationsQueryHandler(ILogger<GetOperationInfoByIdByGroupRelationsQueryHandler> logger, IOperationInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationInfo?>> Handle(GetOperationInfoByIdByGroupRelationsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetOperationInfoByIdByGroupRelations(request.Id, ct);
            return result ?? Result.Failure<OperationInfo>(OperationInfoErrors.OperationInfoWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationInfo>(SharedErrors.UnknownError);
        }
    }
}
