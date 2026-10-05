using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Application.Services.TransportationRequests.Models.GetsFilteredSnap;

namespace Engineering.Application.Services.TransportationRequests.Queries.GetsSnapFiltered;

public class GetsFilteredSnapQueryHandler : IQueryHandler<GetsFilteredSnapQuery, DataResult<List<GetsFilteredSnapResponseModel>>>
{
    private readonly ITransportationRequestRepository _repository;
    private readonly ILogger<GetsFilteredSnapQueryHandler> _logger;

    public GetsFilteredSnapQueryHandler(ILogger<GetsFilteredSnapQueryHandler> logger, ITransportationRequestRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsFilteredSnapResponseModel>>?>> Handle(GetsFilteredSnapQuery request, CT ct)
    {
        try
        {
            var items = await _repository.GetsFilteredSnap(request.Ids,
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
                new DataResult<List<GetsFilteredSnapResponseModel>>
                {
                    Data = items.Data,
                    RowCount = items.RowCount
                } : Result.Failure<DataResult<List<GetsFilteredSnapResponseModel>>>(TransportationRequestErrors.FilteredTransportationRequestNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsFilteredSnapResponseModel>>>(SharedErrors.UnknownError);
        }
    }
}