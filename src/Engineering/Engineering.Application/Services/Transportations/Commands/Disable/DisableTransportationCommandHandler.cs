using Engineering.Application.Abstractions.Data.Transportations;
using Transportation = Engineering.Domain.Entities.Transportations.Transportation;

namespace Engineering.Application.Services.Transportations.Commands.Disable;

public class DisableTransportationCommandHandler : ICommandHandler<DisableTransportationCommand, Transportation>
{
    private readonly ILogger<DisableTransportationCommand> _logger;
    private readonly ITransportationRepository _repository;

    public DisableTransportationCommandHandler(ILogger<DisableTransportationCommand> logger, ITransportationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Transportation?>> Handle(DisableTransportationCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindForDelete(request.Id, ct);
            if (entity is null)
                return Result.Failure<Transportation>(TransportationErrors.TransportationWithIdNotFound);
            if (entity.IsLock != null && entity.IsLock == true)
                return Result.Failure<Transportation>(TransportationErrors.IsLockData);
            if (entity.IsDeleted == true)
                return Result.Failure<Transportation>(TransportationErrors.IsDeleted);

            entity.SetIsDeleted();

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Transportation>(SharedErrors.UnknownError);
        }
    }
}