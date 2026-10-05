using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Commands.SetOperationInfoPriority;

public class SetOperationInfoPriorityCommandHandler : ICommandHandler<SetOperationInfoPriorityCommand, OperationInfo>
{
    private readonly ILogger<SetOperationInfoPriorityCommand> _logger;
    private readonly IOperationInfoRepository _repository;
    private readonly IOperationInfoDependencyRepository _operationInfoDependencyRepository;

    public SetOperationInfoPriorityCommandHandler(ILogger<SetOperationInfoPriorityCommand> logger, IOperationInfoRepository repository, IOperationInfoDependencyRepository operationInfoDependencyRepository)
    {
        _logger = logger;
        _repository = repository;
        _operationInfoDependencyRepository = operationInfoDependencyRepository;
    }

    public async Task<Result<OperationInfo?>> Handle(SetOperationInfoPriorityCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetByIdWithDependencies(request.Id, ct);
            if (entity is null)
                return Result.Failure<OperationInfo>(OperationInfoErrors.OperationInfoWithIdNotFound);

            if (request.SetPriority == 1 && entity.OperationInfoDependencies is not null)
                foreach (var dependency in entity.OperationInfoDependencies)
                    await _operationInfoDependencyRepository.Remove(dependency);

            if (request.SetPriority <= 0)
                entity.SetPriority(null);
            else
                entity.SetPriority(request.SetPriority);

            entity.AddHistory();

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<OperationInfo>(SharedErrors.UnknownError);
        }
    }
}