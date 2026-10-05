using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Application.Services.TransportationRequests.Models.GetsTotalTransportationRequestPrice;

namespace Engineering.Application.Services.TransportationRequests.Queries.GetsTotalTransportationRequestPrice;

public class GetsTotalTransportationRequestPriceQueryHandler : IQueryHandler<GetsTotalTransportationRequestPriceQuery, GetsTotalTransportationRequestPriceResponse>
{
    private readonly ITransportationRequestRepository _repository;
    private readonly ILogger<GetsTotalTransportationRequestPriceQueryHandler> _logger;

    public GetsTotalTransportationRequestPriceQueryHandler(ILogger<GetsTotalTransportationRequestPriceQueryHandler> logger, ITransportationRequestRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetsTotalTransportationRequestPriceResponse?>> Handle(GetsTotalTransportationRequestPriceQuery request, CT ct)
    {
        try
        {
            var price = await _repository.GetsTotalTransportationRequestPrice(
                request.Ids,
                request.CostCenterIds,
                request.ProjectIds,
                request.ProjectOperationIds,
                request.ProjectOperationDetailIds,
                request.CostGroupIds,
                request.CostCategoryIds,
                request.TransportationRequestStatus,
                request.PaymentType,
                request.TripId,
                request.BillOfLadingId,
                request.TransportationId,
                request.RequestById,
                request.StartDate,
                request.EndDate,
                request.RequestNumber,
                request.FromPrice,
                request.ToPrice,
                request.DriverName,
                request.DriverIds,
                request.FilterData,
                request.PageIndex,
                request.PageSize, ct);

            return price;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetsTotalTransportationRequestPriceResponse>(SharedErrors.UnknownError);
        }
    }
}