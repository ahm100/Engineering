using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfoGroup = Engineering.Domain.Entities.OperationInfos.OperationInfoGroup;

namespace Engineering.Application.Services.OperationInfoGroups.Commands.DisableOperationInfoGroup;

public class DisableOperationInfoGroupCommandHandler : ICommandHandler<DisableOperationInfoGroupCommand, OperationInfoGroup>
{
    private readonly ILogger<DisableOperationInfoGroupCommand> _logger;
    private readonly IOperationInfoGroupRepository _repository;

    public DisableOperationInfoGroupCommandHandler(ILogger<DisableOperationInfoGroupCommand> logger, IOperationInfoGroupRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationInfoGroup?>> Handle(DisableOperationInfoGroupCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindForDelete(request.Id, ct);
            if (entity is null)
                return Result.Failure<OperationInfoGroup>(OperationInfoGroupErrors.OperationInfoGroupWithIdNotFound);
            if (entity.IsDeleted == true)
                return Result.Failure<OperationInfoGroup>(OperationInfoGroupErrors.IsDeleted);

            entity.SetIsDeleted();

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