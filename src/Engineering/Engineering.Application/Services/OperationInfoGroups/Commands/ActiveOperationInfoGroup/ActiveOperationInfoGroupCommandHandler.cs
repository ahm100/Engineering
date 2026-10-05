using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfoGroup = Engineering.Domain.Entities.OperationInfos.OperationInfoGroup;

namespace Engineering.Application.Services.OperationInfoGroups.Commands.ActiveOperationInfoGroup;

public class ActiveOperationInfoGroupCommandHandler : ICommandHandler<ActiveOperationInfoGroupCommand, OperationInfoGroup>
{
    private readonly ILogger<ActiveOperationInfoGroupCommand> _logger;
    private readonly IOperationInfoGroupRepository _repository;

    public ActiveOperationInfoGroupCommandHandler(ILogger<ActiveOperationInfoGroupCommand> logger, IOperationInfoGroupRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationInfoGroup?>> Handle(ActiveOperationInfoGroupCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<OperationInfoGroup>(OperationInfoGroupErrors.OperationInfoGroupWithIdNotFound);
            if (entity.IsActive == true)
                return Result.Failure<OperationInfoGroup>(OperationInfoGroupErrors.IsActive);
            if (entity.IsDeleted == true)
                return Result.Failure<OperationInfoGroup>(OperationInfoGroupErrors.IsDeleted);

            entity.SetActive();

            await _repository.Update(entity);
            return entity;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationInfoGroup>(SharedErrors.UnknownError);
        }
    }
}