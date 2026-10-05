using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfoGroup = Engineering.Domain.Entities.OperationInfos.OperationInfoGroup;

namespace Engineering.Application.Services.OperationInfoGroups.Queries.GetsOperationInfoGroup;

public class GetsOperationInfoGroupQueryHandler : IQueryHandler<GetsOperationInfoGroupQuery, DataResult<List<OperationInfoGroup>>>
{
    private readonly IOperationInfoGroupRepository _repository;
    private readonly ILogger<GetsOperationInfoGroupQueryHandler> _logger;

    public GetsOperationInfoGroupQueryHandler(ILogger<GetsOperationInfoGroupQueryHandler> logger, IOperationInfoGroupRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<OperationInfoGroup>>?>> Handle(GetsOperationInfoGroupQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsOperationInfoGroup(request.Ids, request.FilterData, request.IsActive, request.CompanyId, request.OrderBy, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<OperationInfoGroup>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<OperationInfoGroup>>>(ProjectErrors.FilteredProjectNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<OperationInfoGroup>>>(SharedErrors.UnknownError);
        }
    }
}