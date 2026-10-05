using Engineering.Application.Abstractions.Data.FixAssetMachineries;
using MachineryReservation = Engineering.Domain.Entities.FixAssetMachineries.MachineryReservation;

namespace Engineering.Application.Services.MachineryReservations.Queries.GetsMachineryReservationByIds;

public class GetsMachineryReservationByIdsQueryHandler : IQueryHandler<GetsMachineryReservationByIdsQuery, DataResult<List<MachineryReservation?>>>
{
    private readonly IMachineryReservationRepository _repository;
    private readonly ILogger<GetsMachineryReservationByIdsQueryHandler> _logger;

    public GetsMachineryReservationByIdsQueryHandler(ILogger<GetsMachineryReservationByIdsQueryHandler> logger, IMachineryReservationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<MachineryReservation?>>?>> Handle(GetsMachineryReservationByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsMachineryReservationByIds(request.ids, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<MachineryReservation?>>
                {
                    Data = result.Data!,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<MachineryReservation?>>>(MachineryReservationErrors.FilteredMachineryReservationNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<MachineryReservation?>>>(SharedErrors.UnknownError);
        }
    }
}