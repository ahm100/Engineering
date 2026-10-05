using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels;

namespace Engineering.Application.Services.OperationInfos.Queries.GetFilteredOperationInfo;

public class GetOperationInfosQueryHandler : IQueryHandler<GetFilteredOperationInfoQuery, DataResult<List<GetOperationInfosModel>>>
{
    private readonly IOperationInfoRepository _repository;
    private readonly ILogger<GetOperationInfosQueryHandler> _logger;

    public GetOperationInfosQueryHandler(ILogger<GetOperationInfosQueryHandler> logger, IOperationInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetOperationInfosModel>>?>> Handle(GetFilteredOperationInfoQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetFilteredOperationInfo(
                request.Ids,
                request.FilterData,
                request.CategoryId,
                request.BranchId,
                request.SeasonId,
                request.IsActive,
                request.CompanyId,
                request.OrderBy,
                request.PageIndex,
                request.PageSize, ct);

            return result.Data?.Any() ?? false ?
                new DataResult<List<GetOperationInfosModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetOperationInfosModel>>>(OperationInfoErrors.FilteredOperationInfoNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetOperationInfosModel>>>(SharedErrors.UnknownError);
        }
    }
}