using Engineering.Application.Abstractions.Data.Seasons;
using Engineering.Application.Services.Seasons.Models.GetsByBranchIdWhithOperationInfo;

namespace Engineering.Application.Services.Seasons.Queries.GetsByBranchIdWhithOperationInfo;

public class GetsByBranchIdWhithOperationInfoQueryHandler : IQueryHandler<GetsByBranchIdWhithOperationInfoQuery, DataResult<List<GetsByBranchIdWhithOperationInfoModel>>>
{
    private readonly ISeasonRepository _repository;
    private readonly ILogger<GetsByBranchIdWhithOperationInfoQueryHandler> _logger;

    public GetsByBranchIdWhithOperationInfoQueryHandler(ILogger<GetsByBranchIdWhithOperationInfoQueryHandler> logger, ISeasonRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsByBranchIdWhithOperationInfoModel>>?>> Handle(GetsByBranchIdWhithOperationInfoQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsByBranchIdWhithOperationInfoForResponse(request.BranchId, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<GetsByBranchIdWhithOperationInfoModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsByBranchIdWhithOperationInfoModel>>>(SeasonErrors.BranchChildNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsByBranchIdWhithOperationInfoModel>>>(SharedErrors.UnknownError);
        }
    }
}