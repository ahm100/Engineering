using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfoGroup = Engineering.Domain.Entities.OperationInfos.OperationInfoGroup;

namespace Engineering.Application.Services.OperationInfoGroups.Queries.GetsOperationInfoGroupByIds;

public class GetsOperationInfoGroupByIdsQueryHandler : IQueryHandler<GetsOperationInfoGroupByIdsQuery, DataResult<List<OperationInfoGroup>>>
{
    private readonly IOperationInfoGroupRepository _repository;
    private readonly ILogger<GetsOperationInfoGroupByIdsQueryHandler> _logger;

    public GetsOperationInfoGroupByIdsQueryHandler(ILogger<GetsOperationInfoGroupByIdsQueryHandler> logger, IOperationInfoGroupRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<OperationInfoGroup>>?>> Handle(GetsOperationInfoGroupByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetByOperationInfoGroupIds(request.GroupIds, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<OperationInfoGroup>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<OperationInfoGroup>>>(OperationInfoGroupErrors.OperationInfoGroupsNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<OperationInfoGroup>>>(SharedErrors.UnknownError);
        }
    }
}