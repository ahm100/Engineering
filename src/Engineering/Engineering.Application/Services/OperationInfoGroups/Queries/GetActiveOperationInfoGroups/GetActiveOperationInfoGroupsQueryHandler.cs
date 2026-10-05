using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfoGroup = Engineering.Domain.Entities.OperationInfos.OperationInfoGroup;

namespace Engineering.Application.Services.OperationInfoGroups.Queries.GetActiveOperationInfoGroups;

public class GetActiveOperationInfoGroupsQueryHandler : IQueryHandler<GetActiveOperationInfoGroupsQuery, DataResult<List<OperationInfoGroup>>>
{
    private readonly IOperationInfoGroupRepository _repository;
    private readonly ILogger<GetActiveOperationInfoGroupsQuery> _logger;

    public GetActiveOperationInfoGroupsQueryHandler(ILogger<GetActiveOperationInfoGroupsQuery> logger, IOperationInfoGroupRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<OperationInfoGroup>>?>> Handle(GetActiveOperationInfoGroupsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetActiveOperationInfoGroups(request.FilterData, request.code, request.name, request.CompanyId, request.PageIndex, request.PageSize, ct);

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