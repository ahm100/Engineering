using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoSeasons.Queries.GetsByOperationInfoId;

public class GetsByOperationInfoIdQueryHandler : IQueryHandler<GetsByOperationInfoIdQuery, DataResult<List<OperationInfoSeason>>>
{
    private readonly IOperationInfoSeasonRepository _repository;
    private readonly ILogger<GetsByOperationInfoIdQueryHandler> _logger;

    public GetsByOperationInfoIdQueryHandler(ILogger<GetsByOperationInfoIdQueryHandler> logger, IOperationInfoSeasonRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<OperationInfoSeason>>?>> Handle(GetsByOperationInfoIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsByOprationInfoId(request.OprationInfoId, request.PageIndex, request.PageSize, ct);

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
