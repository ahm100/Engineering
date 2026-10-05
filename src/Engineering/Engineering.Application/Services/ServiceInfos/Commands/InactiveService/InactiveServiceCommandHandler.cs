using Engineering.Application.Abstractions.Data.ServiceInfos;
using ServiceInfo = Engineering.Domain.Entities.ServiceInfos.ServiceInfo;

namespace Engineering.Application.Services.ServiceInfos.Commands.InactiveService;

public class InactiveServiceInfoCommandHandler : ICommandHandler<InactiveServiceInfoCommand, ServiceInfo>
{
    private readonly ILogger<InactiveServiceInfoCommand> _logger;
    private readonly IServiceInfoRepository _repository;

    public InactiveServiceInfoCommandHandler(ILogger<InactiveServiceInfoCommand> logger, IServiceInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ServiceInfo?>> Handle(InactiveServiceInfoCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ServiceInfo>(ServiceInfoErrors.ServiceInfoWithIdNotFound);
            if (entity.IsActive == false)
                return Result.Failure<ServiceInfo>(ServiceInfoErrors.IsInactive);

            entity.SetInActive();
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