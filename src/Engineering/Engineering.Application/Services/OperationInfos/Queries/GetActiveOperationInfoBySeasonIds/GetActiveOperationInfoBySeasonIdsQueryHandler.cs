using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Application.Services.OperationInfos.Queries.GetActiveOperationInfos;
using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfos.Queries.GetActiveOperationInfoBySeasonIds;

public class GetActiveOperationInfoBySeasonIdsQueryHandler : IQueryHandler<GetActiveOperationInfoBySeasonIdsQuery, List<OperationInfo?>?>
{
    private readonly IOperationInfoRepository _repository;
    private readonly ILogger<GetActiveOperationInfosQuery> _logger;

    public GetActiveOperationInfoBySeasonIdsQueryHandler(ILogger<GetActiveOperationInfosQuery> logger, IOperationInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<OperationInfo?>?>> Handle(GetActiveOperationInfoBySeasonIdsQuery request, CT ct)
    {
        try
        {
            var data = await _repository.GetActiveOperationInfoBySeasonIds(
                request.CategoryId,
                request.BranchId,
                request.SeasonId,
                request.FilterData,
                request.PageIndex,
                request.PageSize,
                ct);

            if (!data.Any())
                return Result.Failure<List<OperationInfo?>?>(OperationInfoErrors.FilteredOperationInfoNotFound)!;

            return Result.Success(data)!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<OperationInfo?>?>(SharedErrors.UnknownError)!;
        }
    }

}
