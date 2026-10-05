using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Queries.GetsOperationInfoByContractorIds;

public class GetsOperationInfoByContractorIdsQueryHandler : IQueryHandler<GetsOperationInfoByContractorIdsQuery, DataResult<List<OperationInfo>>>
{
    private readonly IOperationInfoRepository _repository;
    private readonly ILogger<GetsOperationInfoByContractorIdsQueryHandler> _logger;

    public GetsOperationInfoByContractorIdsQueryHandler(ILogger<GetsOperationInfoByContractorIdsQueryHandler> logger, IOperationInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<OperationInfo>>?>> Handle(GetsOperationInfoByContractorIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsOperationInfoByContractorIds(request.ContractorIds, request.FilterData, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<OperationInfo>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<OperationInfo>>>(OperationInfoErrors.FilteredOperationInfoNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<OperationInfo>>>(SharedErrors.UnknownError);
        }
    }
}
