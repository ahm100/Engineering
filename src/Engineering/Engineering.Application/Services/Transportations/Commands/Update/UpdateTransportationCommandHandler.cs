using Engineering.Application.Abstractions.Data.Transportations;
using Transportation = Engineering.Domain.Entities.Transportations.Transportation;

namespace Engineering.Application.Services.Transportations.Commands.Update;

public class UpdateTransportationCommandHandler : ICommandHandler<UpdateTransportationCommand, Transportation>
{
    private readonly ILogger<UpdateTransportationCommand> _logger;
    private readonly ITransportationRepository _repository;

    public UpdateTransportationCommandHandler(ILogger<UpdateTransportationCommand> logger, ITransportationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Transportation?>> Handle(UpdateTransportationCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<Transportation>(TransportationErrors.TransportationWithIdNotFound);

            if (entity.IsLock != null && entity.IsLock == true)
                return Result.Failure<Transportation>(TransportationErrors.IsLockData);

            entity.SetName(request.TransportationName);
            entity.SetCode(request.TransportationCode);
            entity.SetIsPassenger(request.IsPassenger);
            entity.SetCompanyId(request.CompanyId);
            entity.SetTransportationType(request.TransportationType);
            if (request.IsActive != entity.IsActive)
            {
                if (request.IsActive == true)
                    entity.SetActive();
                else
                    entity.SetInActive();
            }

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<Transportation>(SharedErrors.UnknownError);
        }
    }
}