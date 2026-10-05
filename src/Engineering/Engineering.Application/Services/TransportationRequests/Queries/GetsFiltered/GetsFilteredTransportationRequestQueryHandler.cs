using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Application.Services.TransportationRequests.Models.GetsFiltered;

namespace Engineering.Application.Services.TransportationRequests.Queries.GetsFiltered;

public class GetsFilteredTransportationRequestQueryHandler : IQueryHandler<GetsFilteredTransportationRequestQuery, DataResult<List<GetsFilteredTransportationRequestResponseModel>>>
{
    private readonly ITransportationRequestRepository _repository;
    private readonly ILogger<GetsFilteredTransportationRequestQueryHandler> _logger;

    public GetsFilteredTransportationRequestQueryHandler(ILogger<GetsFilteredTransportationRequestQueryHandler> logger, ITransportationRequestRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsFilteredTransportationRequestResponseModel>>?>> Handle(GetsFilteredTransportationRequestQuery request, CT ct)
    {
        try
        {
            var items = await _repository.GetsFilteredTransportationRequest(
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
                request.FromDate,
                request.ToDate,
                request.RequestNumber,
                request.FromPrice,
                request.ToPrice,
                request.DriverName,
                request.DriverIds,
                request.FilterData,
                request.OrderBy,
                request.PageIndex,
                request.PageSize,
                ct);

            return items.Data.Any() ?
                new DataResult<List<GetsFilteredTransportationRequestResponseModel>>
                {
                    Data = items.Data,
                    RowCount = items.RowCount
                } : Result.Failure<DataResult<List<GetsFilteredTransportationRequestResponseModel>>>(TransportationRequestErrors.FilteredTransportationRequestNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsFilteredTransportationRequestResponseModel>>>(SharedErrors.UnknownError);
        }
    }
}