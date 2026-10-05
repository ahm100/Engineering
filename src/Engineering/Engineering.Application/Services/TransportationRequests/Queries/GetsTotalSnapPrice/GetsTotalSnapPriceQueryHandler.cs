using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Application.Services.TransportationRequests.Models.GetsTotalSnapPrice;

namespace Engineering.Application.Services.TransportationRequests.Queries.GetsTotalSnapPrice;

public class GetsTotalSnapPriceQueryHandler : IQueryHandler<GetsTotalSnapPriceQuery, GetsTotalSnapPriceResponse>
{
    private readonly ITransportationRequestRepository _repository;
    private readonly ILogger<GetsTotalSnapPriceQueryHandler> _logger;

    public GetsTotalSnapPriceQueryHandler(ILogger<GetsTotalSnapPriceQueryHandler> logger, ITransportationRequestRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetsTotalSnapPriceResponse?>> Handle(GetsTotalSnapPriceQuery request, CT ct)
    {
        try
        {
            var item = await _repository.GetsTotalSnapPrice(request.Ids,
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
            return Result.Failure<GetsTotalSnapPriceResponse>(SharedErrors.UnknownError);
        }
    }
}