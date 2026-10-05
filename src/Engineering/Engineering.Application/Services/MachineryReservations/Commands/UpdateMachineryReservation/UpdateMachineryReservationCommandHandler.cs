using Engineering.Application.Abstractions.Data.FixAssetMachineries;
using Engineering.Domain.Entities.FixAssetMachineries.Enums;
using MachineryReservation = Engineering.Domain.Entities.FixAssetMachineries.MachineryReservation;

namespace Engineering.Application.Services.MachineryReservations.Commands.UpdateMachineryReservation;

public class UpdateMachineryReservationCommandHandler : ICommandHandler<UpdateMachineryReservationCommand, MachineryReservation>
{
    private readonly ILogger<UpdateMachineryReservationCommand> _logger;
    private readonly IMachineryReservationRepository _repository;

    public UpdateMachineryReservationCommandHandler(ILogger<UpdateMachineryReservationCommand> logger, IMachineryReservationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<MachineryReservation?>> Handle(UpdateMachineryReservationCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.Id, null, ct);
            if (entity is null)
                return Result.Failure<MachineryReservation>(MachineryReservationErrors.MachineryReservationNotFoundWithId);

            if (request.FixAssetMachinery.FixAssetMachineryType == FixAssetMachineryType.rented)
                if (!((request.StartDate.Date >= request.FixAssetMachinery.StartDate!.Value.Date && request.StartDate.Date <= request.FixAssetMachinery.EndDate!.Value.Date) &&
                      (request.EndDate.Date >= request.FixAssetMachinery.StartDate!.Value.Date && request.EndDate.Date <= request.FixAssetMachinery.EndDate!.Value.Date)))
                    return Result.Failure<MachineryReservation>(MachineryReservationErrors.DatesAreInFixDates);

            var reservations = request.FixAssetMachinery.MachineryReservations.Where(x => x.Id != entity.Id && x.Status != MachineryReservationStatus.Cancelled).ToList();
            if (reservations != null && reservations.Count > 0)
                if (reservations.Any(x => (request.StartDate >= x.StartDate && request.StartDate <= x.EndDate) ||
                                          (request.EndDate >= x.StartDate && request.EndDate <= x.EndDate)))
                {
                    var reserve = reservations.Where(x => (request.StartDate >= x.StartDate && request.StartDate <= x.EndDate) ||
                                          (request.EndDate >= x.StartDate && request.EndDate <= x.EndDate))
                                        .Select(x => x).FirstOrDefault();

                    var StartDate = TimeCalculator.ConvertToShamsi(reserve!.StartDate.Date);
                    var StartTime = reserve!.StartDate.TimeOfDay;
                    var endDate = TimeCalculator.ConvertToShamsi(reserve!.EndDate.Date);
                    var endTime = reserve!.EndDate.TimeOfDay;

                    return Result.Failure<MachineryReservation>(MachineryReservationErrors
                        .DuplicateDate(StartDate, StartTime, endDate, endTime, request.RequestMachinery));
                }

            entity.SetMachineryReservationUnit(request.MachineryReservationUnit);
            entity.SetRequestMachinery(request.RequestMachinery);
            entity.SetFixAssetMachinery(request.FixAssetMachinery);
            entity.SetStartDate(request.StartDate);
            entity.SetEndDate(request.EndDate);
            entity.SetDescription(request.Description);

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