using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByIdByConsumptionStandards;

public class GetOperationInfoByIdByConsumptionStandardsQueryHandler : IQueryHandler<GetOperationInfoByIdByConsumptionStandardsQuery, OperationInfo>
{
    private readonly ILogger<GetOperationInfoByIdByConsumptionStandardsQueryHandler> _logger;
    private readonly IOperationInfoRepository _repository;

    public GetOperationInfoByIdByConsumptionStandardsQueryHandler(ILogger<GetOperationInfoByIdByConsumptionStandardsQueryHandler> logger, IOperationInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationInfo?>> Handle(GetOperationInfoByIdByConsumptionStandardsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetOperationInfoByIdByConsumptionStandards(request.Id, ct);
            return result ?? Result.Failure<OperationInfo>(OperationInfoErrors.OperationInfoWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationInfo>(SharedErrors.UnknownError);
        }
    }
}