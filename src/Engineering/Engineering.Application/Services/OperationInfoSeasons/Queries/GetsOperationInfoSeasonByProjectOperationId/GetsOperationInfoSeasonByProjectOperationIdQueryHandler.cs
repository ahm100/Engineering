using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoSeasons.Queries.GetsOperationInfoSeasonByProjectOperationId;

public class GetsOperationInfoSeasonByProjectOperationIdQueryHandler : IQueryHandler<GetsOperationInfoSeasonByProjectOperationIdQuery, DataResult<List<OperationInfoSeason>>>
{
    private readonly IOperationInfoSeasonRepository _repository;
    private readonly ILogger<GetsOperationInfoSeasonByProjectOperationIdQueryHandler> _logger;

    public GetsOperationInfoSeasonByProjectOperationIdQueryHandler(ILogger<GetsOperationInfoSeasonByProjectOperationIdQueryHandler> logger, IOperationInfoSeasonRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<OperationInfoSeason>>?>> Handle(GetsOperationInfoSeasonByProjectOperationIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsOperationInfoSeasonByProjectOperationId(request.ProjectOperationId, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<OperationInfoSeason>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<OperationInfoSeason>>>(OperationInfoSeasonErrors.OperationInfoSeasonWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<OperationInfoSeason>>>(SharedErrors.UnknownError);
        }
    }
}
