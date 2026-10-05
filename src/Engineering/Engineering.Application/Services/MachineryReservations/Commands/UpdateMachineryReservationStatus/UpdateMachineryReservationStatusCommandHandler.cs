using Engineering.Application.Abstractions.Data.FixAssetMachineries;
using Engineering.Domain.Entities.FixAssetMachineries.Enums;
using MachineryReservation = Engineering.Domain.Entities.FixAssetMachineries.MachineryReservation;

namespace Engineering.Application.Services.MachineryReservations.Commands.UpdateMachineryReservationStatus;

public class UpdateMachineryReservationStatusCommandHandler : ICommandHandler<UpdateMachineryReservationStatusCommand, MachineryReservation>
{
    private readonly ILogger<UpdateMachineryReservationStatusCommand> _logger;
    private readonly IMachineryReservationRepository _repository;

    public UpdateMachineryReservationStatusCommandHandler(ILogger<UpdateMachineryReservationStatusCommand> logger, IMachineryReservationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<MachineryReservation?>> Handle(UpdateMachineryReservationStatusCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<MachineryReservation>(MachineryReservationErrors.MachineryReservationNotFoundWithId);

            if (request.MachineryReservationStatus == MachineryReservationStatus.Cancelled)
            {
                entity.SetMachineryReservationStatus(request.MachineryReservationStatus);
            }
            else if (request.MachineryReservationStatus == MachineryReservationStatus.InUse && (entity.Status == MachineryReservationStatus.InUse || entity.Status == MachineryReservationStatus.NotsStarted))
            {
                entity.SetMachineryReservationStatus(request.MachineryReservationStatus);
            }
            else if (request.MachineryReservationStatus == MachineryReservationStatus.Finished && entity.Status == MachineryReservationStatus.InUse)
            {
                entity.SetMachineryReservationStatus(request.MachineryReservationStatus);
            }
            else
                return Result.Failure<MachineryReservation>(MachineryReservationErrors.InValidStatus);

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<MachineryReservation>(SharedErrors.UnknownError);
        }
    }
}