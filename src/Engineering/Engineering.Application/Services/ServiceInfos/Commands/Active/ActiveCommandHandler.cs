using Engineering.Application.Abstractions.Data.ServiceInfos;
using ServiceInfo = Engineering.Domain.Entities.ServiceInfos.ServiceInfo;

namespace Engineering.Application.Services.ServiceInfos.Commands.Active;

public class ActiveServiceInfoCommandHandler : ICommandHandler<ActiveServiceInfoCommand, ServiceInfo>
{
    private readonly ILogger<ActiveServiceInfoCommand> _logger;
    private readonly IServiceInfoRepository _repository;

    public ActiveServiceInfoCommandHandler(ILogger<ActiveServiceInfoCommand> logger, IServiceInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ServiceInfo?>> Handle(ActiveServiceInfoCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ServiceInfo>(ServiceInfoErrors.ServiceInfoWithIdNotFound);
            if (entity.IsActive == true)
                return Result.Failure<ServiceInfo>(ServiceInfoErrors.IsActive);

            entity.SetActive();
            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ServiceInfo>(SharedErrors.UnknownError);
        }
    }
}