using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetDailyProjectOperationDetails;

public class GetDailyProjectOperationDetailsQueryHandler : IQueryHandler<GetDailyProjectOperationDetailsQuery, DataResult<List<DailyProjectOperation>>>
{
    private readonly ILogger<GetDailyProjectOperationDetailsQueryHandler> _logger;
    private readonly IDailyProjectOperationRepository _repository;

    public GetDailyProjectOperationDetailsQueryHandler(ILogger<GetDailyProjectOperationDetailsQueryHandler> logger,
                                                       IDailyProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<DailyProjectOperation>>?>> Handle(GetDailyProjectOperationDetailsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetFilteredDailyProjectOperation(request.ProjectOperationDetailId, request.ContractorId,
                request.Length, request.Width, request.Height, request.Weight, request.Number, request.StartDate, request.EndDate,
                request.CreatorId, request.FilterData, request.OrderBy, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<DailyProjectOperation>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<DailyProjectOperation>>>(DailyProjectOperationErrors.DailyProjectOperationFilteredNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<DailyProjectOperation>>>(SharedErrors.UnknownError);
        }
    }
}
