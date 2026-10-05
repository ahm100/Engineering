using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Queries.GetActiveOperationInfos;

public class GetActiveOperationInfosQueryHandler : IQueryHandler<GetActiveOperationInfosQuery, DataResult<List<OperationInfo>>>
{
    private readonly IOperationInfoRepository _repository;
    private readonly ILogger<GetActiveOperationInfosQuery> _logger;

    public GetActiveOperationInfosQueryHandler(ILogger<GetActiveOperationInfosQuery> logger, IOperationInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<OperationInfo>>?>> Handle(GetActiveOperationInfosQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetActiveOperationInfos(request.FilterData, request.CategoryId, request.BranchId, request.SeasonId, request.Priority, request.CompanyId, request.PageIndex, request.PageSize, ct);

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