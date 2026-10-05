using Engineering.Application.Abstractions.Data.ServiceInfos;
using Engineering.Application.Services.ProjectServices.Models.GetsServiceInfoByProjectId;

namespace Engineering.Application.Services.ProjectServices.Queries.GetsServiceInfoByProjectId;

public class GetsServiceInfoByProjectIdQueryHandler : IQueryHandler<GetsServiceInfoByProjectIdQuery, DataResult<List<GetsServiceInfoByProjectIdModel>>>
{
    private readonly IServiceInfoRepository _repository;
    private readonly ILogger<GetsServiceInfoByProjectIdQueryHandler> _logger;

    public GetsServiceInfoByProjectIdQueryHandler(ILogger<GetsServiceInfoByProjectIdQueryHandler> logger, IServiceInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsServiceInfoByProjectIdModel>>?>> Handle(GetsServiceInfoByProjectIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsServiceInfoByProjectId(
                request.ProjectId,
                request.FilterData,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<GetsServiceInfoByProjectIdModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsServiceInfoByProjectIdModel>>>(ProjectServiceErrors.ServiceWithProjectIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsServiceInfoByProjectIdModel>>>(SharedErrors.UnknownError);
        }
    }
}