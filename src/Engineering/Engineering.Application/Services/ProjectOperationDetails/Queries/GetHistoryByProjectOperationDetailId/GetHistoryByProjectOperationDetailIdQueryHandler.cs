using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using ProjectOperationDetailHistory = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetailHistory;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetHistoryByProjectOperationDetailId;

public class GetHistoryByProjectOperationDetailIdQueryHandler : IQueryHandler<GetHistoryByProjectOperationDetailIdQuery, DataResult<List<ProjectOperationDetailHistory>>>
{
    private readonly IProjectOperationDetailHistoryRepository _repository;
    private readonly ILogger<GetHistoryByProjectOperationDetailIdQueryHandler> _logger;

    public GetHistoryByProjectOperationDetailIdQueryHandler(ILogger<GetHistoryByProjectOperationDetailIdQueryHandler> logger, IProjectOperationDetailHistoryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ProjectOperationDetailHistory>>?>> Handle(GetHistoryByProjectOperationDetailIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetHistoryByProjectOperationDetailId(request.Id, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<ProjectOperationDetailHistory>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ProjectOperationDetailHistory>>>(ProjectOperationDetailErrors.HistoryNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ProjectOperationDetailHistory>>>(SharedErrors.UnknownError);
        }
    }
}