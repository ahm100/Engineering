using Engineering.Application.Abstractions.Data.Transportations;
using TransportationRequest = Engineering.Domain.Entities.Transportations.TransportationRequest;

namespace Engineering.Application.Services.TransportationRequests.Commands.Disable;

public class DisableTransportationRequestCommandHandler : ICommandHandler<DisableTransportationRequestCommand, TransportationRequest>
{
    private readonly ILogger<DisableTransportationRequestCommand> _logger;
    private readonly ITransportationRequestRepository _repository;

    public DisableTransportationRequestCommandHandler(ILogger<DisableTransportationRequestCommand> logger, ITransportationRequestRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<TransportationRequest?>> Handle(DisableTransportationRequestCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindForDelete(request.Id, ct);
            if (entity is null)
                return Result.Failure<TransportationRequest>(TransportationRequestErrors.TransportationRequestWithIdNotFound);
            if (entity.IsDeleted == true)
                return Result.Failure<TransportationRequest>(TransportationRequestErrors.IsDeleted);

            entity.SetIsDeleted();

            entity.AddHistory();

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<TransportationRequest>(SharedErrors.UnknownError);
        }
    }
}