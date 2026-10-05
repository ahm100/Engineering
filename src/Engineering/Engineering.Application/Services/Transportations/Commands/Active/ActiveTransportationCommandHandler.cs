using Engineering.Application.Abstractions.Data.Transportations;
using Transportation = Engineering.Domain.Entities.Transportations.Transportation;

namespace Engineering.Application.Services.Transportations.Commands.Active;

public class ActiveTransportationCommandHandler : ICommandHandler<ActiveTransportationCommand, Transportation>
{
    private readonly ILogger<ActiveTransportationCommand> _logger;
    private readonly ITransportationRepository _repository;

    public ActiveTransportationCommandHandler(ILogger<ActiveTransportationCommand> logger, ITransportationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Transportation?>> Handle(ActiveTransportationCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<Transportation>(TransportationErrors.TransportationWithIdNotFound);
            if (entity.IsLock != null && entity.IsLock == true)
                return Result.Failure<Transportation>(TransportationErrors.IsLockData);
            if (entity.IsActive == true)
                return Result.Failure<Transportation>(TransportationErrors.IsActive);
            if (entity.IsDeleted == true)
                return Result.Failure<Transportation>(TransportationErrors.IsDeleted);

            entity.SetActive();
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