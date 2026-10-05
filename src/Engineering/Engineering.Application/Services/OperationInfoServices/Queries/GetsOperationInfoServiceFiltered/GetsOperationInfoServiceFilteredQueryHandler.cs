using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoServices.Queries.GetsOperationInfoServiceFiltered;

public class GetsOperationInfoServiceFilteredQueryHandler : IQueryHandler<GetsOperationInfoServiceFilteredQuery, DataResult<List<OperationInfoService>>>
{
    private readonly IOperationInfoServiceRepository _repository;
    private readonly ILogger<GetsOperationInfoServiceFilteredQueryHandler> _logger;

    public GetsOperationInfoServiceFilteredQueryHandler(ILogger<GetsOperationInfoServiceFilteredQueryHandler> logger, IOperationInfoServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<OperationInfoService>>?>> Handle(GetsOperationInfoServiceFilteredQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsOperationInfoServiceFiltered(request.CategoryId, request.BranchId, request.SeasonId, request.FilterData, request.CompanyId, request.OrderBy, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<OperationInfoService>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<OperationInfoService>>>(OperationInfoServiceErrors.FilteredOperationInfoServiceNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<OperationInfoService>>>(SharedErrors.UnknownError);
        }
    }
}
