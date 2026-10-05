using Engineering.Application.Abstractions.Data.Transportations;
using Transportation = Engineering.Domain.Entities.Transportations.Transportation;

namespace Engineering.Application.Services.Transportations.Commands.Inactive;

public class InactiveTransportationCommandHandler : ICommandHandler<InactiveTransportationCommand, Transportation>
{
    private readonly ILogger<InactiveTransportationCommand> _logger;
    private readonly ITransportationRepository _repository;

    public InactiveTransportationCommandHandler(ILogger<InactiveTransportationCommand> logger, ITransportationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Transportation?>> Handle(InactiveTransportationCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<Transportation>(TransportationErrors.TransportationWithIdNotFound);
            if (entity.IsLock != null && entity.IsLock == true)
                return Result.Failure<Transportation>(TransportationErrors.IsLockData);
            if (entity.IsActive == false)
                return Result.Failure<Transportation>(TransportationErrors.IsInactive);
            if (entity.IsDeleted == true)
                return Result.Failure<Transportation>(TransportationErrors.IsDeleted);

            entity.SetInActive();
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