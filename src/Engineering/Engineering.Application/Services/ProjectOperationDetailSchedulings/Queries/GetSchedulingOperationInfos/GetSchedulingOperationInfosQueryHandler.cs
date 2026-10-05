using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.ProjectOperationDetailSchedulings.Queries.GetSchedulingProjectOperations;

public class GetSchedulingOperationInfosQueryHandler : IQueryHandler<GetSchedulingOperationInfosQuery, DataResult<List<OperationInfo>>>
{
    private readonly ILogger<GetSchedulingOperationInfosQueryHandler> _logger;
    private readonly IProjectOperationDetailRepository _repository;

    public GetSchedulingOperationInfosQueryHandler(ILogger<GetSchedulingOperationInfosQueryHandler> logger, IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<OperationInfo>>?>> Handle(GetSchedulingOperationInfosQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetOperationInfoForSchecdulingAsync(request.CostCenterId, request.ProjectId, request.FilterData,
                request.OperationLocationIds, request.OperationInfoIds, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<OperationInfo>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<OperationInfo>>>(OperationInfoErrors.OperationInfoWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<OperationInfo>>>(SharedErrors.UnknownError);
        }
    }
}
