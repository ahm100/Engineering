using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByIdIncludeLess;

public class GetOperationInfoByIdIncludeLessQueryHandler : IQueryHandler<GetOperationInfoByIdIncludeLessQuery, OperationInfo>
{
    private readonly ILogger<GetOperationInfoByIdIncludeLessQueryHandler> _logger;
    private readonly IOperationInfoRepository _repository;

    public GetOperationInfoByIdIncludeLessQueryHandler(ILogger<GetOperationInfoByIdIncludeLessQueryHandler> logger, IOperationInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationInfo?>> Handle(GetOperationInfoByIdIncludeLessQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetOperationInfoByIdIncludeLess(request.Id, ct);
            return result ?? Result.Failure<OperationInfo>(OperationInfoErrors.OperationInfoWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationInfo>(SharedErrors.UnknownError);
        }
    }
}