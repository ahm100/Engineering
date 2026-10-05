using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Application.Services.TransportationRequests.Models.GetsFilteredAirplane;

namespace Engineering.Application.Services.TransportationRequests.Queries.GetsAirplaneFiltered;

public class GetsFilteredAirplaneQueryHandler : IQueryHandler<GetsFilteredAirplaneQuery, DataResult<List<GetsFilteredAirplaneResponseModel>>>
{
    private readonly ITransportationRequestRepository _repository;
    private readonly ILogger<GetsFilteredAirplaneQueryHandler> _logger;

    public GetsFilteredAirplaneQueryHandler(ILogger<GetsFilteredAirplaneQueryHandler> logger, ITransportationRequestRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsFilteredAirplaneResponseModel>>?>> Handle(GetsFilteredAirplaneQuery request, CT ct)
    {
        try
        {
            var items = await _repository.GetsFilteredAirplane(request.Ids,
                   request.CostCenterIds,
                   request.ProjectIds,
                   request.ProjectOperationIds,
                   request.ProjectOperationDetailIds,
                   request.CostGroupIds,
                   request.CostCategoryIds,
                   request.TripIds,
                   request.TransportationIds,
                   request.PassengerIds,
                   request.RequestById,
                   request.TransportationRequestStatus,
                   request.PaymentType,
                   request.StartDate,
                   request.EndDate,
                   request.FromCreateDate,
                   request.ToCreateDate,
                   request.RequestNumber,
                   request.FromPrice,
                   request.ToPrice,
                   request.DriverName,
                   request.FilterData,
                   request.OrderBy,
                   request.PageIndex,
                   request.PageSize, ct);

            return items.Data.Any() ?
                new DataResult<List<GetsFilteredAirplaneResponseModel>>
                {
                    Data = items.Data,
                    RowCount = items.RowCount
                } : Result.Failure<DataResult<List<GetsFilteredAirplaneResponseModel>>>(TransportationRequestErrors.FilteredTransportationRequestNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsFilteredAirplaneResponseModel>>>(SharedErrors.UnknownError);
        }
    }
}