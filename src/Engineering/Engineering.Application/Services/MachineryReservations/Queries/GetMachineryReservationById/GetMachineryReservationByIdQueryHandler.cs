using Engineering.Application.Abstractions.Data.FixAssetMachineries;
using MachineryReservation = Engineering.Domain.Entities.FixAssetMachineries.MachineryReservation;

namespace Engineering.Application.Services.MachineryReservations.Queries.GetMachineryReservationById;

public class GetMachineryReservationByIdQueryHandler : IQueryHandler<GetMachineryReservationByIdQuery, MachineryReservation?>
{
    private readonly ILogger<GetMachineryReservationByIdQueryHandler> _logger;
    private readonly IMachineryReservationRepository _repository;

    public GetMachineryReservationByIdQueryHandler(ILogger<GetMachineryReservationByIdQueryHandler> logger, IMachineryReservationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<MachineryReservation?>> Handle(GetMachineryReservationByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetById(request.Id, null, ct);

            return result ?? Result.Failure<MachineryReservation?>(MachineryReservationErrors.MachineryReservationNotFoundWithId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<MachineryReservation?>(SharedErrors.UnknownError);
        }
    }
}