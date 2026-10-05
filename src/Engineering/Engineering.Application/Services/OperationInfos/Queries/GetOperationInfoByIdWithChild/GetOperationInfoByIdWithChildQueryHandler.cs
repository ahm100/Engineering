using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByIdWithChild;

public class GetOperationInfoByIdWithChildQueryHandler : IQueryHandler<GetOperationInfoByIdWithChildQuery, OperationInfo>
{
    private readonly ILogger<GetOperationInfoByIdWithChildQueryHandler> _logger;
    private readonly IOperationInfoRepository _repository;

    public GetOperationInfoByIdWithChildQueryHandler(ILogger<GetOperationInfoByIdWithChildQueryHandler> logger, IOperationInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationInfo?>> Handle(GetOperationInfoByIdWithChildQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetOperationInfoByIdWithChild(request.Id, ct);
            return result ?? Result.Failure<OperationInfo>(OperationInfoErrors.OperationInfoWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationInfo>(SharedErrors.UnknownError);
        }
    }
}