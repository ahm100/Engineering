using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByName;

public class GetOperationInfoByNameQueryHandler : IQueryHandler<GetOperationInfoByNameQuery, OperationInfo>
{
    private readonly ILogger<GetOperationInfoByNameQueryHandler> _logger;
    private readonly IOperationInfoRepository _repository;

    public GetOperationInfoByNameQueryHandler(ILogger<GetOperationInfoByNameQueryHandler> logger, IOperationInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationInfo?>> Handle(GetOperationInfoByNameQuery request, CT ct)
    {
        try
        {
            var result = await _repository.FindByName(request.OperationInfoName, request.UnitOfMeasurementId, request.CompanyId, ct);
            return result ?? Result.Failure<OperationInfo>(OperationInfoErrors.OperationInfoWithNameNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationInfo>(SharedErrors.UnknownError);
        }
    }
}