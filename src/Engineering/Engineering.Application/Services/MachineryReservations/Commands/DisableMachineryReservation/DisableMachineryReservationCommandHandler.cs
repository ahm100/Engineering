using Engineering.Application.Abstractions.Data.FixAssetMachineries;
using MachineryReservation = Engineering.Domain.Entities.FixAssetMachineries.MachineryReservation;

namespace Engineering.Application.Services.MachineryReservations.Commands.DisableMachineryReservation;

public class DisableMachineryReservationCommandHandler : ICommandHandler<DisableMachineryReservationCommand, MachineryReservation>
{
    private readonly ILogger<DisableMachineryReservationCommand> _logger;
    private readonly IMachineryReservationRepository _repository;

    public DisableMachineryReservationCommandHandler(ILogger<DisableMachineryReservationCommand> logger, IMachineryReservationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<MachineryReservation?>> Handle(DisableMachineryReservationCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetMachineryReservationForDelete(request.Id, ct);
            if (entity is null)
                return Result.Failure<MachineryReservation>(MachineryReservationErrors.MachineryReservationNotFoundWithId);
            if (entity.IsDeleted == true)
                return Result.Failure<MachineryReservation>(MachineryReservationErrors.IsDeleted);

            entity.SetIsDeleted();

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<MachineryReservation>(SharedErrors.UnknownError);
        }
    }
}