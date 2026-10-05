using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByIdByExperts;

public class GetOperationInfoByIdByExpertsQueryHandler : IQueryHandler<GetOperationInfoByIdByExpertsQuery, OperationInfo>
{
    private readonly ILogger<GetOperationInfoByIdByExpertsQueryHandler> _logger;
    private readonly IOperationInfoRepository _repository;

    public GetOperationInfoByIdByExpertsQueryHandler(ILogger<GetOperationInfoByIdByExpertsQueryHandler> logger, IOperationInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationInfo?>> Handle(GetOperationInfoByIdByExpertsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetOperationInfoByIdByExperts(request.Id, ct);
            return result ?? Result.Failure<OperationInfo>(OperationInfoErrors.OperationInfoWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationInfo>(SharedErrors.UnknownError);
        }
    }
}
