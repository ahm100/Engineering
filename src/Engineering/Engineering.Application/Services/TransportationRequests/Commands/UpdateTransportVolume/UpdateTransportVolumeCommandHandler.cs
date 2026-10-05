using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Domain.Entities.Transportations;

namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateTransportVolume;

public class UpdateTransportVolumeCommandHandler : ICommandHandler<UpdateTransportVolumeCommand, TransportationRequest>
{
    private readonly ILogger<UpdateTransportVolumeCommand> _logger;
    private readonly ITransportationRequestRepository _repository;

    public UpdateTransportVolumeCommandHandler(
        ILogger<UpdateTransportVolumeCommand> logger,
        ITransportationRequestRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<TransportationRequest?>> Handle(UpdateTransportVolumeCommand request, CT ct)
    {
        try
        {
            var entity = request.TransportationRequest;

            entity.SetVolume(request.Volume);

            entity.AddHistory();

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<TransportationRequest>(SharedErrors.UnknownError);
        }
    }
}