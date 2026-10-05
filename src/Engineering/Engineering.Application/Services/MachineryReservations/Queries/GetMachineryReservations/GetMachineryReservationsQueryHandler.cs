using Engineering.Application.Abstractions.Data.FixAssetMachineries;
using MachineryReservation = Engineering.Domain.Entities.FixAssetMachineries.MachineryReservation;

namespace Engineering.Application.Services.MachineryReservations.Queries.GetMachineryReservations;

public class GetMachineryReservationsQueryHandler : IQueryHandler<GetMachineryReservationsQuery, DataResult<List<MachineryReservation>>>
{
    private readonly IMachineryReservationRepository _repository;
    private readonly ILogger<GetMachineryReservationsQueryHandler> _logger;

    public GetMachineryReservationsQueryHandler(ILogger<GetMachineryReservationsQueryHandler> logger, IMachineryReservationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<MachineryReservation>>?>> Handle(GetMachineryReservationsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetMachineryReservations(
                    request.Ids,
                    request.MachineryIds,
                    request.FixAssetMachineryIds,
                    request.RequestMachineryIds,
                    request.Unit,
                    request.Status,
                    request.StartDate,
                    request.EndDate,
                    request.FilterData,
                    request.OrderBy,
                    request.PageIndex,
                    request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<MachineryReservation>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<MachineryReservation>>>(MachineryReservationErrors.FilteredMachineryReservationNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<MachineryReservation>>>(SharedErrors.UnknownError);
        }
    }
}