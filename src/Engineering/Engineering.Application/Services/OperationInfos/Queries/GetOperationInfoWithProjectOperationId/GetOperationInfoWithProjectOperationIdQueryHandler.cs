using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoWithProjectOperationId;

public class GetOperationInfoWithProjectOperationIdQueryHandler : IQueryHandler<GetOperationInfoWithProjectOperationIdQuery, OperationInfo>
{
    private readonly ILogger<GetOperationInfoWithProjectOperationIdQueryHandler> _logger;
    private readonly IOperationInfoRepository _repository;

    public GetOperationInfoWithProjectOperationIdQueryHandler(ILogger<GetOperationInfoWithProjectOperationIdQueryHandler> logger, IOperationInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationInfo?>> Handle(GetOperationInfoWithProjectOperationIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetOperationInfoWithProjectOperationId(request.Id, ct);
            return result ?? Result.Failure<OperationInfo>(OperationInfoErrors.OperationInfoWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationInfo>(SharedErrors.UnknownError);
        }
    }
}