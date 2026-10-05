using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfoDependency = Engineering.Domain.Entities.OperationInfos.OperationInfoDependency;

namespace Engineering.Application.Services.OperationInfoDependencies.Commands.DisableOperationInfoDependency;

public class DisableOperationInfoDependencyCommandHandler : ICommandHandler<DisableOperationInfoDependencyCommand, OperationInfoDependency>
{
    private readonly ILogger<DisableOperationInfoDependencyCommandHandler> _logger;
    private readonly IOperationInfoDependencyRepository _repository;

    public DisableOperationInfoDependencyCommandHandler(ILogger<DisableOperationInfoDependencyCommandHandler> logger, IOperationInfoDependencyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationInfoDependency?>> Handle(DisableOperationInfoDependencyCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindForDelete(request.Id, ct);
            if (entity is null)
                return Result.Failure<OperationInfoDependency>(OperationInfoErrors.OperationInfoDependencyWithIdNotFound);
            if (entity.IsDeleted == true)
                return Result.Failure<OperationInfoDependency>(OperationInfoErrors.IsDeletedDependency);

            entity.SetIsDeleted();
            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationInfoDependency>(SharedErrors.UnknownError);
        }
    }
}