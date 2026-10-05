using Engineering.Application.Abstractions.Data.FixAssetMachineries;
using Engineering.Domain.Entities.FixAssetMachineries.Enums;
using Engineering.Domain.Entities.RequestMachineries.Enums;
using MachineryReservation = Engineering.Domain.Entities.FixAssetMachineries.MachineryReservation;

namespace Engineering.Application.Services.MachineryReservations.Commands.CreateMachineryReservation;

public class CreateMachineryReservationCommandHandler : ICommandHandler<CreateMachineryReservationCommand, MachineryReservation?>
{
    private readonly ILogger<CreateMachineryReservationCommand> _logger;
    private readonly IMachineryReservationRepository _repository;

    public CreateMachineryReservationCommandHandler(ILogger<CreateMachineryReservationCommand> logger, IMachineryReservationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<MachineryReservation?>> Handle(CreateMachineryReservationCommand request, CT ct)
    {
        try
        {
            if (request.FixAssetMachinery.FixAssetMachineryType == FixAssetMachineryType.rented)
                if (request.FixAssetMachinery.StartDate != null && request.FixAssetMachinery.EndDate != null)
                    if (!((request.StartDate.Date >= request.FixAssetMachinery.StartDate!.Value.Date && request.StartDate.Date <= request.FixAssetMachinery.EndDate!.Value.Date) &&
                          (request.EndDate.Date >= request.FixAssetMachinery.StartDate!.Value.Date && request.EndDate.Date <= request.FixAssetMachinery.EndDate!.Value.Date)))
                        return Result.Failure<MachineryReservation>(MachineryReservationErrors.DatesAreInFixDates);

            var reservations = request.FixAssetMachinery.MachineryReservations.Where(x => x.Status != MachineryReservationStatus.Cancelled).ToList();
            if (reservations != null && reservations.Count > 0)
                if (reservations.Any(x => ((request.StartDate > x.StartDate && request.StartDate < x.EndDate) ||
                                          (request.EndDate > x.StartDate && request.EndDate < x.EndDate)) &&
                                          x.RequestMachinery.Status == RequestMachineryStatus.OnProject))
                {
                    var reserve = reservations.Where(x => (request.StartDate > x.StartDate && request.StartDate < x.EndDate) ||
                                           (request.EndDate > x.StartDate && request.EndDate < x.EndDate))
                                        .Select(x => x).FirstOrDefault();

                    var StartDate = TimeCalculator.ConvertToShamsi(reserve!.StartDate.Date);
                    var StartTime = reserve!.StartDate.TimeOfDay;
                    var endDate = TimeCalculator.ConvertToShamsi(reserve!.EndDate.Date);
                    var endTime = reserve!.EndDate.TimeOfDay;

                    return Result.Failure<MachineryReservation>(MachineryReservationErrors
                        .DuplicateDate(StartDate, StartTime, endDate, endTime, reserve.RequestMachinery));
                }

            if (request.FixAssetMachinery.FixAssetMachineryNotWorks
                .Any(x => (request.StartDate > x.StartDate && request.StartDate < x.EndDate) ||
                          (request.EndDate > x.StartDate && request.EndDate < x.EndDate)))
                return Result.Failure<MachineryReservation>(MachineryReservationErrors.CantWork);

            var newMachineryReservation = new MachineryReservation(request.StartDate, request.EndDate, request.Description, request.MachineryReservationUnit,
                request.FixAssetMachinery, request.RequestMachinery);
            var result = await _repository.Create(newMachineryReservation, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<MachineryReservation?>(SharedErrors.UnknownError);
        }
    }
}