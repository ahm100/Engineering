using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByCode;

public class GetOperationInfoByCodeQueryHandler : IQueryHandler<GetOperationInfoByCodeQuery, OperationInfo>
{
    private readonly ILogger<GetOperationInfoByCodeQueryHandler> _logger;
    private readonly IOperationInfoRepository _repository;

    public GetOperationInfoByCodeQueryHandler(ILogger<GetOperationInfoByCodeQueryHandler> logger, IOperationInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationInfo?>> Handle(GetOperationInfoByCodeQuery request, CT ct)
    {
        try
        {
            var result = await _repository.FindByCode(request.OperationInfoCode, request.CompanyId, ct);
            return result ?? Result.Failure<OperationInfo>(OperationInfoErrors.OperationInfoWithCodeNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationInfo>(SharedErrors.UnknownError);
        }
    }
}