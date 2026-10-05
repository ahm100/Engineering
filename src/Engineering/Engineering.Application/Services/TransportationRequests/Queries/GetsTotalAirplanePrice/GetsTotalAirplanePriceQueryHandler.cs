using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Application.Services.TransportationRequests.Models.GetsTotalAirplanePrice;

namespace Engineering.Application.Services.TransportationRequests.Queries.GetsTotalAirplanePrice;

public class GetsTotalAirplanePriceQueryHandler : IQueryHandler<GetsTotalAirplanePriceQuery, GetsTotalAirplanePriceResponse>
{
    private readonly ITransportationRequestRepository _repository;
    private readonly ILogger<GetsTotalAirplanePriceQueryHandler> _logger;

    public GetsTotalAirplanePriceQueryHandler(ILogger<GetsTotalAirplanePriceQueryHandler> logger, ITransportationRequestRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetsTotalAirplanePriceResponse?>> Handle(GetsTotalAirplanePriceQuery request, CT ct)
    {
        try
        {
            var item = await _repository.GetsTotalAirplanePrice(request.Ids,
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
                   request.PageIndex,
                   request.PageSize, ct);

            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetsTotalAirplanePriceResponse>(SharedErrors.UnknownError);
        }
    }
}