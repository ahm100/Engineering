using Engineering.Application.Abstractions.Data.ServiceInfos;
using ServiceInfo = Engineering.Domain.Entities.ServiceInfos.ServiceInfo;

namespace Engineering.Application.Services.ServiceInfos.Commands.DeleteServiceInfo;

public class DeleteServiceInfoCommandHandler : ICommandHandler<DeleteServiceInfoCommand, ServiceInfo>
{
    private readonly ILogger<DeleteServiceInfoCommand> _logger;
    private readonly IServiceInfoRepository _repository;

    public DeleteServiceInfoCommandHandler(ILogger<DeleteServiceInfoCommand> logger, IServiceInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ServiceInfo?>> Handle(DeleteServiceInfoCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ServiceInfo>(ServiceInfoErrors.ServiceInfoWithIdNotFound);
            if (entity.IsDeleted)
                return Result.Failure<ServiceInfo>(ServiceInfoErrors.IsDeleted);
            if (entity.OperationInfoServices.Where(x => !x.IsDeleted).Count() > 0)
                return Result.Failure<ServiceInfo>(ServiceInfoErrors.CanNotDeleteForOperationInfoServices);

            entity.SetIsDeleted();
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