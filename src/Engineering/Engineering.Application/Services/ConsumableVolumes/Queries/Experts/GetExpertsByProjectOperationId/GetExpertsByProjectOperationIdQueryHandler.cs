using Engineering.Application.Abstractions.Data.ProjectOperationDetails.ConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Models.Experts.DataModels;

namespace Engineering.Application.Services.ConsumableVolumes.Queries.Experts.GetExpertsByProjectOperationId;

public class GetExpertsByProjectOperationIdQueryHandler : IQueryHandler<GetExpertsByProjectOperationIdQuery, DataResult<List<ExpertsDataModel>>>
{
    private readonly IConsumableVolumeExpertRepository _repository;
    private readonly ILogger<GetExpertsByProjectOperationIdQueryHandler> _logger;

    public GetExpertsByProjectOperationIdQueryHandler(ILogger<GetExpertsByProjectOperationIdQueryHandler> logger, IConsumableVolumeExpertRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ExpertsDataModel>>?>> Handle(GetExpertsByProjectOperationIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsByProjectOperationId(request.ProjectOperationId, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<ExpertsDataModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ExpertsDataModel>>>(ProjectOperationErrors.ProjectChildNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ExpertsDataModel>>>(SharedErrors.UnknownError);
        }
    }
}