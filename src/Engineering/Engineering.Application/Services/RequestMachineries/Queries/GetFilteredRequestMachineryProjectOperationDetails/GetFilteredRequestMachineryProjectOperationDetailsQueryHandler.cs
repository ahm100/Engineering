using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Queries.GetFilteredRequestMachineryProjectOperationDetails;

public class GetFilteredRequestMachineryProjectOperationDetailsQueryHandler : IQueryHandler<GetFilteredRequestMachineryProjectOperationDetailsQuery, DataResult<List<RequestMachinery>>>
{
    private readonly ILogger<GetFilteredRequestMachineryProjectOperationDetailsQueryHandler> _logger;
    private readonly IRequestMachineryRepository _repository;

    public GetFilteredRequestMachineryProjectOperationDetailsQueryHandler(ILogger<GetFilteredRequestMachineryProjectOperationDetailsQueryHandler> logger, IRequestMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<RequestMachinery>>?>> Handle(GetFilteredRequestMachineryProjectOperationDetailsQuery request, CT ct)
    {
        try
        {
            var entities = await _repository.GetFilteredForDailyAsync(request.ProjectOperationDetailId, request.FilterData, request.OrderBy, request.PageIndex, request.PageSize, ct);

            return entities.Data.Any()
                ? new DataResult<List<RequestMachinery>>
                {
                    Data = entities.Data,
                    RowCount = entities.RowCount
                } : Result.Failure<DataResult<List<RequestMachinery>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<RequestMachinery>>>(SharedErrors.UnknownError);
        }
    }
}
