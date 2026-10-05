using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfoDependency = Engineering.Domain.Entities.OperationInfos.OperationInfoDependency;

namespace Engineering.Application.Services.OperationInfoDependencies.Commands.UpdateOperationInfoDependency;

public class UpdateOperationInfoDependencyCommandHandler : ICommandHandler<UpdateOperationInfoDependencyCommand, OperationInfoDependency>
{
    private readonly ILogger<UpdateOperationInfoDependencyCommand> _logger;
    private readonly IOperationInfoDependencyRepository _repository;

    public UpdateOperationInfoDependencyCommandHandler(ILogger<UpdateOperationInfoDependencyCommand> logger, IOperationInfoDependencyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationInfoDependency?>> Handle(UpdateOperationInfoDependencyCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<OperationInfoDependency>(OperationInfoDependencyErrors.DependencyWithIdNotFound);

            entity.SetOperationInfo(request.OperationInfo);
            entity.SetWorkingDays(request.WorkingDays);
            entity.SetDependencyType(request.DependencyType);

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<OperationInfoDependency>(SharedErrors.UnknownError);
        }
    }
}