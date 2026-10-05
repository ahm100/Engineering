using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByIdByMachineries;

public class GetOperationInfoByIdByMachineriesQueryHandler : IQueryHandler<GetOperationInfoByIdByMachineriesQuery, OperationInfo>
{
    private readonly ILogger<GetOperationInfoByIdByMachineriesQueryHandler> _logger;
    private readonly IOperationInfoRepository _repository;

    public GetOperationInfoByIdByMachineriesQueryHandler(ILogger<GetOperationInfoByIdByMachineriesQueryHandler> logger, IOperationInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationInfo?>> Handle(GetOperationInfoByIdByMachineriesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetOperationInfoByIdByMachineries(request.Id, ct);
            return result ?? Result.Failure<OperationInfo>(OperationInfoErrors.OperationInfoWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationInfo>(SharedErrors.UnknownError);
        }
    }
}