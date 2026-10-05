using Engineering.Application.Abstractions.Data.FixAssetMachineries;

namespace Engineering.Application.Services.RequestMachineryManagements.Commands.DeleteRequestMachineryReservations;

public class DeleteRequestMachineryReservationsCommandHandler : ICommandHandler<DeleteRequestMachineryReservationsCommand, bool?>
{
    private readonly ILogger<DeleteRequestMachineryReservationsCommandHandler> _logger;
    private readonly IMachineryReservationRepository _repository;

    public DeleteRequestMachineryReservationsCommandHandler(ILogger<DeleteRequestMachineryReservationsCommandHandler> logger, IMachineryReservationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool?>> Handle(DeleteRequestMachineryReservationsCommand request, CT ct)
    {
        try
        {
            var entities = request.RequestMachinery.MachineryReservations.ToList();

            if (entities is not null)
                if (entities.Count > 0)
                    foreach (var item in entities)
                    {
                        item.SetIsDeleted();
                        await _repository.Update(item);
                    }

            return true;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool?>(SharedErrors.UnknownError);
        }
    }
}
